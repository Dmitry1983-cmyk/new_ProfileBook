using Acr.UserDialogs;
using newProfileBook.Services.Repository;
using newProfileBook.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media.Abstractions;

namespace newProfileBook.ViewModel
{
    public class AddEditProfileViewModel : BindableBase
    {
        public string _title;
        public string _nickname;
        public string _name;
        public string _cdescription;
        private string imagePath;

        private readonly INavigationService _navigateService;
        private IUserDialogs _userDialogs;
        private IMedia _media;
        private IRepository _repository;
        private ObservableCollection<ProfileModel> _profileList;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Nickname
        {
            get { return _nickname; }
            set { SetProperty(ref _nickname, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Description
        {
            get { return _cdescription; }
            set
            {
                SetProperty(ref _cdescription, value);
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                SetProperty(ref imagePath, value);
            }
        }

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        
        #region--ctor
        public AddEditProfileViewModel(INavigationService navigationService, IRepository repository)
        {
            Title = "Add Profile Page";
            ImagePath = "pic_profile.png";
            _navigateService = navigationService;
            _repository = repository;
        }
        #endregion


        #region--methods

        public ICommand AddProfile => new Command(ExecuteNavigateCommand);
        private async void ExecuteNavigateCommand()
        {
            var profile = new ProfileModel()
            {
                Nickname = Nickname,
                Name = Name,
                ImagePath= "pic_profile.png",
                Description = Description,
                CreationTime = DateTime.Now
            };

            await _repository.InsertAsync(profile);
            await _navigateService.NavigateAsync(nameof(MainListPageView));
        }



        public ICommand ImageCommand => new Command(OnImageCommand);
        private void OnImageCommand()
        {
            
            _userDialogs = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();

            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption("Gallery", () => FromGalleryAsync(), "ic_collections_black.png"));
            Options.Add(new ActionSheetOption("Camera", () => FromCameraAsync(), "ic_camera_alt_black.png"));
            ActionSheetOption cancel = new ActionSheetOption("Cancel", null, null);

            config.Options = Options;
            config.Cancel = cancel;

            _userDialogs.ActionSheet(config);
        }

        public async void FromGalleryAsync()
        {
            var image = await _media.PickPhotoAsync();
            if (image != null)
            {
                ImagePath = image.Path;
            }
            
        }
        public async void FromCameraAsync()
        {
            var image = await _media.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Name = "CamPic.jpg"
            });
            if (image != null)
            {
                ImagePath = image.Path;
            }
        }

        


        #endregion

    }
}

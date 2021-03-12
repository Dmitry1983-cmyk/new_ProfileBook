using Acr.UserDialogs;
using newProfileBook.Services.Repository;
using newProfileBook.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using System.IO;
using Prism.Commands;
using newProfileBook.Model;
using newProfileBook.Services.Settings;
using newProfileBook.Services;
using newProfileBook.RESX;

namespace newProfileBook.ViewModel
{
    public class AddEditProfileViewModel : ViewModelBase
    {
        public string _title;
        public int _id;
        public string _nickname;
        public string _name;
        public string _cdescription;
        private string imagePath;
        private Profile profile;
        ImageSource photoImageSource;

        private readonly IProfileService _profileService;
        private IUserDialogs _userDialogs;
        private IMedia _media;


        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
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

        public ImageSource PhotoImageSource
        {
            set
            {
                SetProperty(ref photoImageSource, value);
            }
            get => photoImageSource;
        }

        public Profile Profile
        {
            get { return profile; }
            set
            {
                SetProperty(ref profile, value);
            }
        }


        #region--ctor

        public AddEditProfileViewModel(INavigationService navigationService, IUserDialogs userDialogs,
            IMedia media, ISettingsUsers settingsUsers, IProfileService profileService):base(navigationService,settingsUsers)
        {
            Title = "Add profile";
            PhotoImageSource = "pic_profile.png";
            _navigationService = navigationService;
            _settingsUsers = settingsUsers;
            _profileService = profileService;
            _userDialogs = userDialogs;
            _media = media;
            Profile = new Profile();
        }
        #endregion


        #region--methods

        public ICommand AddProfile => new Command(ExecuteNavigateCommand);
        private async void ExecuteNavigateCommand()
        {
            if (Title == "Edit profile")
            {
                if (Nickname.Length != 0 && Name.Length != 0)
                {
                    Profile.New_Id = _settingsUsers.CurrentUser;
                    Profile.Name = Name;
                    Profile.Nickname = Nickname;
                    Profile.Description = Description;
                    Profile.DateTime = DateTime.Now;
                    Profile.ImgPath = ImagePath;
                    _profileService.SaveProfile(Profile);

                    await _navigationService.NavigateAsync(nameof(MainListPageView));
                }
                else
                {
                    await _userDialogs.ActionSheetAsync(null, Resources["LoginIsTooLong"], "Ok");
                }
            }
            else
            {
                Title = "Add profile";
                var profile = new Profile()
                {
                    Nickname = Nickname,
                    Name = Name,
                    ImgPath = ImagePath,
                    Description = Description,
                    DateTime = DateTime.Now
                };
                if(profile.ImgPath==null)
                {
                    profile.ImgPath = "pic_profile.png";
                }
                if (!string.IsNullOrEmpty(Nickname) && !string.IsNullOrEmpty(Name))
                {

                    _profileService.SaveProfile(profile);

                    await _navigationService.NavigateAsync(nameof(MainListPageView));
                }
                else
                {
                    await _userDialogs.ActionSheetAsync(null, Resources["LoginIsTooShort"], "Ok");
                }
            }

        }


        public ICommand ImageCommand => new Command(OnImageCommand);
        private void OnImageCommand()
        {
            
            _userDialogs = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();

            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption(Resources["Gallery"], () => FromGalleryAsync(), "ic_collections_black.png"));
            Options.Add(new ActionSheetOption(Resources["Camera"], () => FromCameraAsync(), "ic_camera_alt_black.png"));
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
                PhotoImageSource = ImageSource.FromStream(() => image.GetStream());
            }
            
        }
        public async void FromCameraAsync()
        {
            var image = await _media.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Name = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg",
                SaveToAlbum=true
            });
            
            if (image != null)
            {
                ImagePath = image.Path;
                PhotoImageSource = ImageSource.FromStream(() => image.GetStream());
            }
        }

       
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var profile = parameters.GetValue<Profile>("profile");
            if (profile != null)
            { 
                Id = profile.New_Id;
                Title = "Edit profile";
                Nickname = profile.Nickname;
                Name = profile.Name;
                Description = profile.Description;
                Profile = profile;
                ImagePath = profile.ImgPath;
                var bytes = File.ReadAllBytes(profile.ImgPath);
                PhotoImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            else
            {
                Title = "Add profile";
                PhotoImageSource = "pic_profile.png";
            }
        }


        #endregion
    }
}

using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;
using System.Collections.ObjectModel;
using newProfileBook.Services.Repository;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Acr.UserDialogs;
using newProfileBook.Model;
using newProfileBook.Services;
using newProfileBook.ViewModel;
using newProfileBook.Services.Settings;
using Rg.Plugins.Popup.Services;
using newProfileBook.PopUpsView;

namespace newProfileBook
{
    class MainListPageViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;

        private ObservableCollection<Profile> _profileList;

        #region---property

        public ObservableCollection<Profile> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion

        #region --ctor
        public MainListPageViewModel(INavigationService navigationService, ISettingsUsers settingsUsers,
            IProfileService profileService) : base(navigationService, settingsUsers)
        {
            Title = "Main List Page";
            _settingsUsers = settingsUsers;
            _profileService = profileService;
            Print();
        }

        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Print();
        }

        #region--methods

        public ICommand EditCommand => new Command<Profile>(OnAddEditTappedCommandAsync);
        private async void OnAddEditTappedCommandAsync(Profile profile)
        {
            var param = new NavigationParameters();
            param.Add("profile", profile);
            await _navigationService.NavigateAsync(nameof(AddEditProfileView), param);
        }

        public ICommand OnTapAddUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            await _navigationService.NavigateAsync($"{nameof(AddEditProfileView)}");
        }


        public ICommand LogOutTappedCommand =>new Command(OnLogOutCommandAsync);
        private async void OnLogOutCommandAsync()
        {
            _settingsUsers.CurrentUser = -1;
            await _navigationService.NavigateAsync("/NavigationPage/MainPage");
        }

        public ICommand SettingsCommand => new Command(OnSettingsTappedCommandAsync);
        private async void OnSettingsTappedCommandAsync()
        {
            await _navigationService.NavigateAsync(nameof(SettingsPage));
        }

        public ICommand RemoveCommand => new Command<Profile>(OnRemoveTappedCommandAsync);
        private async void OnRemoveTappedCommandAsync(Profile profile)
        {
            var query = await App.Current.MainPage.DisplayAlert("Delete Profile", "Are you want to delete " + profile.Nickname + " ?", "Ok", "Cancel");
            if (query)
            {
                _profileService.DeleteProfile(profile.Id);
                ProfileList.Remove(profile);
            }
        }

        public void Print()
        {
            ProfileList = new ObservableCollection<Profile>(_profileService.SortProfiles());
        }


        public ICommand ProfileSelected => new Command<Profile>(OnProfileSelectedCommandAsync);
        private async void OnProfileSelectedCommandAsync(Profile profile)
        {
            await PopupNavigation.Instance.PushAsync(new ListPopUpView(profile.ImgPath));
        }
        #endregion

    }
}

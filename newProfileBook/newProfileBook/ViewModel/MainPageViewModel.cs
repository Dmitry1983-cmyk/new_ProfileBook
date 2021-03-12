using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;
using Acr.UserDialogs;
using newProfileBook.Services.Repository;
using SQLite;
using System.IO;
using System;
using newProfileBook.Services.Authentitication;
using newProfileBook.Services.Authorization;
using newProfileBook.Model;
using newProfileBook.ViewModel;
using newProfileBook.Services.Settings;

namespace newProfileBook
{
    public class MainPageViewModel: ViewModelBase
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;

        public string Login{ get; set; }
        public string Password{ get; set; }


        #region--ctor
        public MainPageViewModel(INavigationService navigationService, ISettingsUsers settingsUsers, IAuthorizationService authorizationService,
            IUserDialogs userDialogs) : base(navigationService,settingsUsers)
        {
            Title = "Main Page";
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
        }

        #endregion

        #region --method
        public ICommand OnTapSignUpPage => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPageView));
        }

        public ICommand OnTapLogin => new Command(ExecuteNavigateCommand_MainList);
        async private void ExecuteNavigateCommand_MainList()
        {
            var chek = _authorizationService.Authorizate(Login, Password);
            if (chek ==1)
            {
                await _navigationService.NavigateAsync(nameof(MainListPageView));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(null, Resources["Error"], "Ok");
                Password = string.Empty;
                RaisePropertyChanged($"{nameof(Password)}");
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("user", out string login))
            {
                Login = login;
                RaisePropertyChanged($"{nameof(Login)}");
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

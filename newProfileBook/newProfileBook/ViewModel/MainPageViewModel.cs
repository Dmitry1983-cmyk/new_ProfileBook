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

namespace newProfileBook
{
    public class MainPageViewModel: ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigateService;
        private readonly IUserDialogs _userDialogs;

        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;

        private User _user;
        public string _login;
        public string _password;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        #region--ctor
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, 
            IAuthorizationService authorizationService, IUserDialogs userDialogs) : base(navigationService)
        {
            Title = "Main Page";
            User = new User();
            _navigateService = navigationService;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
        }

        #endregion

        #region --method
        public ICommand OnTapSignUpPage => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            await _navigateService.NavigateAsync(nameof(RegisterPageView));
        }

        public ICommand OnTapLogin => new Command(ExecuteNavigateCommand_MainList);
        async private void ExecuteNavigateCommand_MainList()
        {
            int id = _authenticationService.Authenticate(User.Login, User.Password);//dm_i_try 12345678 qwer 12345678
            if (id != 0)//изменить на !=0  -не корректно работает
            {
                _authorizationService.Authorizate(id);
                await _navigateService.NavigateAsync(nameof(MainListPageView));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Incorrect Login or Password", "OK");
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var profile = parameters.GetValue<User>("user");
            if (profile != null)
            {
                Login = profile.Login;
                Password = profile.Password;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

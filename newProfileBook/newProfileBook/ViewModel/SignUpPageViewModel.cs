using Acr.UserDialogs;
using newProfileBook.Model;
using newProfileBook.Services.Authentitication;
using newProfileBook.Services.Authorization;
using newProfileBook.Services.Repository;
using newProfileBook.ViewModel;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace newProfileBook
{
    public class SignUpPageViewModel : BindableBase //BindableBase ViewModelBase
    {
        private readonly INavigationService _navigateService;

        private readonly IRepository<User> _repository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;
        //private readonly IUserDialogs _userDialogs;

        public string _title;
        public string _login;
        public string _password;
        public string _confirm;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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

        public string Confirm
        {
            get { return _confirm; }
            set { SetProperty(ref _confirm, value); }
        }

        public SignUpPageViewModel(INavigationService navigationService, IRepository<User> repository, IAuthenticationService authenticationService)
        {
            Title = "Sign Up Page";
            _navigateService = navigationService;
            _repository = repository;

            _authenticationService = authenticationService;
        }
        //public SignUpPageViewModel(INavigationService navigationService, IRepository<User> repository, 
        //    IAuthenticationService authenticationService, IAuthorizationService authorizationService)
        //{
        //    Title = "Sign Up Page";
        //    _navigateService = navigationService;
        //    _repository = repository;
        //    _authorizationService = authorizationService;
        //    _authenticationService = authenticationService;
        //}
        public ICommand OnTapRegisterUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
            {
                if (Login.Length > 3 && Login.Length < 17)
                {
                    if(Password.Length>7 && Password.Length<17)
                    {
                        if(Password==Confirm)
                        {
                            int query = _authenticationService.Authenticate(Login, Password);
                            if (query == 0)
                            {
                                var user = new User()
                                {
                                    Login = Login,
                                    Password = Password,
                                    Confirm = Confirm,
                                };

                                var param = new NavigationParameters();
                                param.Add("user", user);

                                 _repository.InsertItem(user);
                                await _navigateService.NavigateAsync(nameof(MainPage), param);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert(null, "User Exists", "OK");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert(null, "password or confirm field incorrect", "Ok");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(null, "Password at be least 8 and no more 16 letter", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(null, "Login at be least 4 and no more 16 letter", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(null, "fields cannot be empty", "Ok");
            }
            
            
        }
    }
}
 

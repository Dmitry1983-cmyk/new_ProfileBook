using Acr.UserDialogs;
using newProfileBook.Model;
using newProfileBook.Services.Authentitication;
using newProfileBook.Services.Authorization;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
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
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        public string Login { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }


        public SignUpPageViewModel(INavigationService navigationService, ISettingsUsers settingsUsers,
            IAuthenticationService authenticationService) : base(navigationService, settingsUsers)
        {
            Title = "Sign Up Page";
            _navigationService = navigationService;
            _settingsUsers = settingsUsers;
            _authenticationService = authenticationService;
        }
        public ICommand OnTapRegisterUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            switch (_authenticationService.Validate(Login, Password, Confirm))
            {
                case Validator.LoginIsTaken:
                    await App.Current.MainPage.DisplayAlert(null, "User Exist. Try Again", "OK");
                    break;
                case Validator.LoginIsTooLong:
                    await App.Current.MainPage.DisplayAlert(null, "Login Is Too Long", "OK");
                    break;
                case Validator.LoginIsTooShort:
                    await App.Current.MainPage.DisplayAlert(null, "Login Is Too Short", "OK");
                    break;
                case Validator.LoginStartsWithNumber:
                    await App.Current.MainPage.DisplayAlert(null, "Login Starts With Number", "OK");
                    break;
                case Validator.PasswordIsTooLong:
                    await App.Current.MainPage.DisplayAlert(null, "Password Is Too Long", "OK");
                    break;
                case Validator.PasswordIsTooShort:
                    await App.Current.MainPage.DisplayAlert(null, "Password Is Too Short", "OK");
                    break;
                case Validator.PasswordIsWeak:
                    await App.Current.MainPage.DisplayAlert(null, "Password must contains at least one uppercase letter, one lowercase letter and one number", "OK");
                    break;
                case Validator.PasswordsAreNotEqual:
                    await App.Current.MainPage.DisplayAlert(null, "Passwords Are Not Equal", "OK");
                    break;
                case Validator.Success:
                    await App.Current.MainPage.DisplayAlert(null, "Register Succesfully", "OK");
                    RegistrationSuccess();
                    break;
            }
        }

        private async void RegistrationSuccess()
        {
            _authenticationService.RegistrationUser(Login, Password);
            var param = new NavigationParameters();
            param.Add("user", Login);
            await _navigationService.NavigateAsync(nameof(MainPage), param);
        }
    }
}


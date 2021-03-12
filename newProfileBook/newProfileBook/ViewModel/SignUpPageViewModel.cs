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
using newProfileBook.RESX;

namespace newProfileBook
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;
        public string Login { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }


        public SignUpPageViewModel(INavigationService navigationService, ISettingsUsers settingsUsers,
            IAuthenticationService authenticationService, IUserDialogs userDialogs) : base(navigationService, settingsUsers)
        {
            Title = "Sign Up Page";
            _navigationService = navigationService;
            _settingsUsers = settingsUsers;
            _authenticationService = authenticationService;
            _userDialogs = userDialogs;
        }
        public ICommand OnTapRegisterUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            switch (_authenticationService.Validate(Login, Password, Confirm))
            {
                case Validator.LoginIsTaken:
                    await _userDialogs.AlertAsync(Resources["LoginIsTaken"], null, "OK");
                    break;
                case Validator.LoginIsTooLong:
                    await _userDialogs.AlertAsync( Resources["LoginIsTooLong"], null, "OK");
                    break;
                case Validator.LoginIsTooShort:
                    await _userDialogs.AlertAsync(Resources["LoginIsTooShort"], null, "OK");
                    break;
                case Validator.LoginStartsWithNumber:
                    await _userDialogs.AlertAsync(Resources["LoginStartsWithNumber"], null, "OK");
                    break;
                case Validator.PasswordIsTooLong:
                    await _userDialogs.AlertAsync(Resources["PasswordIsTooLong"], null, "OK");
                    break;
                case Validator.PasswordIsTooShort:
                    await _userDialogs.AlertAsync(Resources["PasswordIsTooShort"], null, "OK");
                    break;
                case Validator.PasswordIsWeak:
                    await _userDialogs.AlertAsync(Resources["PasswordIsWeak"], null, "OK");
                    break;
                case Validator.PasswordsAreNotEqual:
                    await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], null, "OK");
                    break;
                case Validator.Success:
                    await _userDialogs.AlertAsync(Resources["Success"], null, "OK");
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


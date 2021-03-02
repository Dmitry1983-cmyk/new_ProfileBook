using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace newProfileBook
{
    public class SignUpPageViewModel: BindableBase
    {
        private INavigationService _navigateService;

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

        public SignUpPageViewModel(INavigationService navigationService)
        {
            Title = "Sign Up Page";
            _navigateService = navigationService;
        }
        public ICommand OnTapRegisterUser => new Command(ExecuteNavigateCommand);

        async private void ExecuteNavigateCommand()
        {
            await _navigateService.NavigateAsync($"{nameof(MainPage)}");
        }
    }
}
 

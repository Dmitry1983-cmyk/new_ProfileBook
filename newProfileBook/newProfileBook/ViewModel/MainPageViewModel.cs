using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using newProfileBook.View;
using Acr.UserDialogs;

namespace newProfileBook
{
    public class MainPageViewModel: BindableBase
    {
        private readonly INavigationService _navigateService;
        private IUserDialogs _userDialogs;

        public string _title;
        public string _login;
        public string _password;

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
        #region--ctor
        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Main Page";
            _navigateService = navigationService;
        }

        #endregion

        #region --method
        public ICommand OnTapSignUpPage => new Command(ExecuteNavigateCommand);

        async private void ExecuteNavigateCommand()
        {
            await _navigateService.NavigateAsync($"{nameof(RegisterPageView)}");
        }

        public ICommand OnTapLogin => new Command(ExecuteNavigateCommand_MainList);

        async private void ExecuteNavigateCommand_MainList()
        {
            await _navigateService.NavigateAsync($"{nameof(MainListPageView)}");
        }

        #endregion
    }
}

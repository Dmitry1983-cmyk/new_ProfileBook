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

namespace newProfileBook
{
    public class MainPageViewModel: BindableBase, INavigationAware
    {
        private readonly INavigationService _navigateService;
        private IUserDialogs _userDialogs;
        IRepository _repository;

        public string _title;
        public string _login;
        public string _password;
        public int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

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

        IAuthenticationService _authenticationService;
        IAuthorizationService _authorizationService;

        #region--ctor
        public MainPageViewModel(INavigationService navigationService, IRepository repository, IAuthenticationService authenticationService)//, IAuthorizationService authorizationService
        {
            Title = "Main Page";
            _navigateService = navigationService;
            _repository = repository;

            _authenticationService = authenticationService;
            //_authorizationService = authorizationService;
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
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profilebook_2.db3");
            var database = new SQLiteAsyncConnection(path);

            var user = database.Table<ProfileModel>().Where(x => x.Login == Login && x.Password == Password).FirstOrDefaultAsync();
            if (user != null)
            {

                await _navigateService.NavigateAsync(nameof(MainListPageView));//login/pass Vasya 

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Incorrect Login or Password", "OK");
            }

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var profile = parameters.GetValue<ProfileModel>("user");
            if (profile != null)
            {
                Id = profile.Id;
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

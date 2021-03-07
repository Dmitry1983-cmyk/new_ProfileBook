using Acr.UserDialogs;
using newProfileBook.Services.Authentitication;
using newProfileBook.Services.Authorization;
using newProfileBook.Services.Repository;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace newProfileBook
{
    public class SignUpPageViewModel: BindableBase
    {
        private INavigationService _navigateService;
        IRepository _repository;
        IUserDialogs _userDialogs;

        public string _title;
        public string _login;
        public string _password;
        public string _confirm;

        public string _nickname;
        public string _name;
        public string _cdescription;
        private string imagePath;

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

        IAuthenticationService _authenticationService;

        public SignUpPageViewModel(INavigationService navigationService, IRepository repository, IAuthenticationService authenticationService)
        {
            Title = "Sign Up Page";
            _navigateService = navigationService;
            _repository = repository;

            _authenticationService = authenticationService;
        }
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
                                var user = new ProfileModel()
                                {
                                    Login = Login,
                                    Password = Password,
                                    Confirm = Confirm,

                                    Name = Login,
                                    Nickname = Login,
                                    Description = Login,
                                    ImagePath = "pic_profile.png"
                                };

                                var param = new NavigationParameters();
                                param.Add("user", user);

                                await _repository.InsertAsync(user);
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
 

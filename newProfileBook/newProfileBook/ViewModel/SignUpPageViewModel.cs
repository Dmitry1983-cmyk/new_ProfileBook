using Acr.UserDialogs;
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

        public SignUpPageViewModel(INavigationService navigationService, IRepository repository)
        {
            Title = "Sign Up Page";
            _navigateService = navigationService;
            _repository = repository;

        }
        public ICommand OnTapRegisterUser => new Command(ExecuteNavigateCommand);
        async private void ExecuteNavigateCommand()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profilebook_2.db3");
            var database = new SQLiteAsyncConnection(path);
            var data = database.Table<ProfileModel>();
            var txt_log_pass = data.Where(x => x.Login == Login && x.Password == Password).FirstOrDefaultAsync();

            if (Login.Length < 9 && Login.Length != 0)
            {
                if (Password.Length != 0 && Password.Length < 16)
                {
                    if (Password == Confirm)
                    {
                        if (txt_log_pass != null)
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
                            await _repository.InsertAsync(user);
                            await _navigateService.NavigateAsync($"{nameof(MainPage)}");
                        }
                        else
                        {
                             await App.Current.MainPage.DisplayAlert("Warning", "Incorrect Login or Password", "OK");
                        }
                    }
                }
            }

        }
    }
}
 

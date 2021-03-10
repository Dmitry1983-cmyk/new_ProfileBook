using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using newProfileBook.Model;
using Prism.Navigation;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
using Acr.UserDialogs;

namespace newProfileBook.ViewModel
{
    class SettingsPageViewModel : BindableBase, INavigationAware
    {
        private readonly ISettingsUsers _settingsUsers;
        private readonly IRepository<User> _repository;
        private readonly IUserDialogs _userDialogs;
        private Sorted _sorted;
        private string _title;
        private bool _sortName;
        private bool _sortNickname;
        private bool _sortDate;

        private OSAppTheme _theme;
        private bool _lightTheme;
        private bool _darkTheme;


        #region--prop

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool SortByName
        {
            get { return _sortName; }
            set
            {
                _sortName = value;
                if(value==true)
                {
                    _sorted = Sorted.Name;
                    SaveSorting();
                    SetProperty(ref _sortName, value);
                }
            }
        }

        public bool SortByNickName
        {
            get { return _sortNickname; }
            set
            {
                _sortNickname = value;
                if (value == true)
                {
                    _sorted = Sorted.Nickname;
                    SaveSorting();
                    SetProperty(ref _sortNickname, value);
                }
            }
        }

        public bool SortByDateTime
        {
            get { return _sortDate; }
            set
            {
                _sortDate = value;
                if (value == true)
                {
                    _sorted = Sorted.Nickname;
                    SaveSorting();
                    SetProperty(ref _sortDate, value);
                }
            }
        }

        public bool LightTheme
        {
            get { return _lightTheme; }
            set
            {
                _lightTheme = value;
                if (value == true)
                {
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                    _theme = OSAppTheme.Light;
                    SaveTheme();
                    SetProperty(ref _lightTheme, value);
                }
            }
        }
        public bool DarkTheme
        {
            get { return _darkTheme; }
            set
            {
                _darkTheme = value;
                if (value == true)
                {
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                    _theme = OSAppTheme.Dark;
                    SaveTheme();
                    SetProperty(ref _darkTheme, value);
                }
            }
        }

        #endregion


        #region----ctor


        public SettingsPageViewModel(IRepository<User> repository, IUserDialogs userDialogs, ISettingsUsers settingsUsers)
        {
            Title = "Setting Page";
            _repository = repository;
            _userDialogs = userDialogs;
            _settingsUsers = settingsUsers;
        }


        #endregion

        #region---methods
        private void SaveSorting()
        {
            try
            {
                _settingsUsers.Sorting = (int)_sorted;
                _userDialogs.ActionSheetAsync(null, "Sorted", "Ok");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _userDialogs.ActionSheetAsync(null, e.ToString(), "Ok");
            }
            
        }

        void SetSorting()
        {
            switch ((int)_sorted)
            {
                case 0:
                    {
                        SortByDateTime = true;
                        break;
                    }
                case 1:
                    {
                        SortByNickName = true;
                        break;
                    }
                case 2:
                    {
                        SortByName = true;
                        break;
                    }
                default: break;
            }
        }

        private void SaveTheme()
        {
            _settingsUsers.Theme = (int)_theme;
        }
        void SetTheme()
        {
            switch ((int)_theme)
            {
                case 1:
                    {
                        LightTheme = true;
                        break;
                    }
                case 2:
                    {
                        DarkTheme = true;
                        break;
                    }
                default: break;
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _sorted = (Sorted)_settingsUsers.Sorting;
            SetSorting();
            _theme = (OSAppTheme)_settingsUsers.Theme;
            SetTheme();
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using Xamarin.Forms;
using newProfileBook.Model;
using Prism.Navigation;
using newProfileBook.Services.Settings;
using Acr.UserDialogs;
using System.Collections.Generic;
using newProfileBook.RESX;
using newProfileBook.Localization;
using System.Globalization;

namespace newProfileBook.ViewModel
{
    class SettingsPageViewModel : ViewModelBase
    {
        private bool _sortName;
        private bool _sortNickname;
        private bool _sortDate;

        private bool _chekTheme;
        private bool _chekLang;

        #region--prop

        public bool SortByName
        {
            get { return _sortName; }
            set
            {
                _sortName = value;
                if(value==true)
                {
                    _settingsUsers.Sorting = (int)Sorted.Name;
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
                    _settingsUsers.Sorting = (int)Sorted.Nickname;
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
                    _settingsUsers.Sorting = (int)Sorted.DateTime;
                }
            }
        }

        public bool CheckTemeDarkLight
        {
            get { return _chekTheme; }
            set
            {
                _chekTheme = value;
                if (value == true)
                {
                    _settingsUsers.Theme = (int)OSAppTheme.Dark;
                }
                else
                {
                    _settingsUsers.Theme= (int)OSAppTheme.Light;
                }
            }
        }

        private string _selectedLanguage;
        public string CheckLanguageEngRus
        {
            get
            { return _selectedLanguage; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _selectedLanguage = value;
                }
                _settingsUsers.Language = CheckLanguageEngRus;
            }
        }
        public List<string> Languages { get; set; }

        #endregion


        #region----ctor


        public SettingsPageViewModel(ISettingsUsers settingsUsers, 
            INavigationService navigationService) : base(navigationService, settingsUsers)
        {
            Languages = new List<string>(System.Enum.GetNames(typeof(Languages)));
            CheckLanguageEngRus = _settingsUsers.Language;
        }


        #endregion

        #region---methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            switch(_settingsUsers.Sorting)
            {
                case (int)Sorted.DateTime:
                    SortByDateTime = true;
                    RaisePropertyChanged(nameof(SortByDateTime));
                    break;
                case (int)Sorted.Nickname:
                    SortByNickName = true;
                    RaisePropertyChanged(nameof(SortByNickName));
                    break;
                case (int)Sorted.Name:
                    SortByName = true;
                    RaisePropertyChanged(nameof(SortByName));
                    break;
                default:
                    break;
            }

            if(Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                CheckTemeDarkLight = true;
                RaisePropertyChanged(nameof(CheckTemeDarkLight));
            }
        }

        #endregion
    }
}

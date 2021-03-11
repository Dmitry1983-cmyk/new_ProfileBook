using Xamarin.Forms;
using newProfileBook.Model;
using Prism.Navigation;
using newProfileBook.Services.Settings;
using Acr.UserDialogs;

namespace newProfileBook.ViewModel
{
    class SettingsPageViewModel : ViewModelBase
    {
        private IUserDialogs _userDialogs;
        private bool _sortName;
        private bool _sortNickname;
        private bool _sortDate;

        private bool _chekTheme;


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

        #endregion


        #region----ctor


        public SettingsPageViewModel(IUserDialogs userDialogs,
            ISettingsUsers settingsUsers, INavigationService navigationService) : base(navigationService, settingsUsers)
        {
            Title = "Setting Page";
            _userDialogs = userDialogs;
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

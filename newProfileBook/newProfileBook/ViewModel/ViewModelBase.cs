using newProfileBook.Localization;
using newProfileBook.RESX;
using newProfileBook.Services.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace newProfileBook.ViewModel
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware
    {
        protected INavigationService _navigationService { get; set; }
        protected ISettingsUsers _settingsUsers { get; set; }

        public LocalizedResources Resources
        {
            get;
            private set;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ViewModelBase(INavigationService navigationService, ISettingsUsers settingsUsers)
        {
            _navigationService = navigationService;
            _settingsUsers = settingsUsers;
            Resources = new LocalizedResources(typeof(AppResources), _settingsUsers.Language);
        }

        public virtual void Initialize(INavigationParameters parameters){}

        public virtual void OnNavigatedFrom(INavigationParameters parameters){}

        public virtual void OnNavigatedTo(INavigationParameters parameters){}

        public virtual void Destroy(){}
    }
}

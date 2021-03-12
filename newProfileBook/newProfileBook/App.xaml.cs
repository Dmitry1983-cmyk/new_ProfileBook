using Acr.UserDialogs;
using newProfileBook.Services.Authentitication;
using newProfileBook.Services.Authorization;
using newProfileBook.Services.Repository;
using newProfileBook.Services.Settings;
using newProfileBook.View;
using newProfileBook.ViewModel;
using Plugin.Media;
using Plugin.Settings;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using newProfileBook.Model;
using newProfileBook.Services;

namespace newProfileBook
{
    public partial class App 
    {
        private ISettingsUsers settingsUsers;
        public App():this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            settingsUsers = Container.Resolve<ISettingsUsers>();

            if (settingsUsers.CurrentUser == -1)
            {
                NavigationService.NavigateAsync("NavigationPage/MainPage");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/MainListPageView");
                Application.Current.UserAppTheme = (OSAppTheme)settingsUsers.Theme;
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPageView,SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPageView, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossMedia.Current); 
            containerRegistry.RegisterInstance(CrossSettings.Current);

            containerRegistry.RegisterSingleton<ISettingsUsers, SettingsUsers>();
            containerRegistry.RegisterSingleton<IRepository<User>, Repository<User>>();
            containerRegistry.RegisterSingleton<IRepository<Profile>, Repository<Profile>>();
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<IProfileService, ProfileService>();
        }
    }
}

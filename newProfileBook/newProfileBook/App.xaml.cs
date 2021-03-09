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

namespace newProfileBook
{
    public partial class App 
    {
        public App():this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
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

            containerRegistry.RegisterSingleton<IRepository, Repository>();
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<ISettingsUsers, SettingsUsers>();

            //containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());// Prism.Ioc.ContainerResolutionException:
            //containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            //containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            //containerRegistry.RegisterInstance<ISettingsUsers>(Container.Resolve<SettingsUsers>());
        }
    }
}

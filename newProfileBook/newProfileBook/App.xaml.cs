using newProfileBook.Services.Repository;
using newProfileBook.View;
using newProfileBook.ViewModel;
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
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPageView,SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPageView, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());//надо сделать прослойку, обратится к ней, а потом прослойка идет к Репозиторию
        }
    }
}

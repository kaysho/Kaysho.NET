using Kaysho.NET.Mobile.Services;
using Kaysho.NET.Mobile.ViewModels;
using Kaysho.NET.Mobile.ViewModels.Article;
using Kaysho.NET.Mobile.ViewModels.Login;
using Kaysho.NET.Mobile.ViewModels.Signup;
using Kaysho.NET.Mobile.Views.Article;
using Kaysho.NET.Mobile.Views.ArticleList;
using Kaysho.NET.Mobile.Views.Login;
using Kaysho.NET.Mobile.Views.Signup;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Kaysho.NET.Mobile
{
    public partial class App
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;



        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();


            InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            await NavigationService.NavigateAsync("LoginPage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<ArticleWithCommentsPage, ArticleWithCommentsPageViewModel>();

            containerRegistry.RegisterForNavigation<ArticleListPage, ArticleListPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

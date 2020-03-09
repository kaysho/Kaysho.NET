using Autofac;
using Kaysho.NET.Core.Contracts.Repository;
using Kaysho.NET.Core.Contracts.Services.Data;
using Kaysho.NET.Core.Repository;
using Kaysho.NET.Mobile.Contracts.Services.Data;
using Kaysho.NET.Mobile.Contracts.Services.General;
using Kaysho.NET.Mobile.Services;
using Kaysho.NET.Mobile.ViewModels;
using Kaysho.NET.Mobile.ViewModels.Article;
using Kaysho.NET.Mobile.ViewModels.Login;
using Kaysho.NET.Mobile.ViewModels.Profile;
using Kaysho.NET.Mobile.ViewModels.Signup;
using Prism.Navigation;
using System;

namespace Kaysho.NET.Mobile.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //Viewmodels
            builder.RegisterType<ArticleWithCommentsPageViewModel>();
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<ViewModels.Login.LoginViewModel>();
            builder.RegisterType<ChatProfileViewModel>();
            builder.RegisterType<SignUpPageViewModel>();
            builder.RegisterType<ArticleListPageViewModel>();
            builder.RegisterType<ItemDetailViewModel>();
            builder.RegisterType<ItemsViewModel>();
            builder.RegisterType<INavigationService>();
            builder.RegisterType<ViewModels.Signup.LoginViewModel>();


            //services - data
            builder.RegisterType<BlogDataService>().As<IBlogDataService>();

            //services - general
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();


            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}

using StreamingTinder.Services;
using StreamingTinder.Views;
using StreaminTinderClassLibrary.Users;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreamingTinder
{
    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton<StreaminTinderClassLibrary.Users.IUserService>(UserServiceFactory.GetUserService());

            DependencyService.Register<MockDataStore>();
           // MainPage = new AppShell();

            var nav = new NavigationPage(new AboutPage());
            MainPage = nav;
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
    }
}

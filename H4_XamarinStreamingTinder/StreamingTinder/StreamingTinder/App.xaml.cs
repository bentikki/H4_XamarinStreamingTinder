using Acr.UserDialogs;
using StreamingTinder.Data;
using StreamingTinder.Models;
using StreamingTinder.Services;
using StreamingTinder.Views;
using StreamingTinderClassLibrary;
using StreamingTinderClassLibrary.Rooms;
using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreamingTinder
{
    public partial class App : Application
    {
        static UserDatabase localUserDatabase;

        // Create the database connection as a singleton.
        public static UserDatabase LocalUserDatabase
        {
            get
            {
                if (localUserDatabase == null)
                {
                    localUserDatabase = new UserDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return localUserDatabase;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton<StreaminTinderClassLibrary.Users.IUserService>(ServiceFactory.GetUserService());
            DependencyService.RegisterSingleton<IStreamingPlatformService>(ServiceFactory.GetStreamingPlatformService());
            DependencyService.RegisterSingleton<IRoomService>(ServiceFactory.GetRoomService());

            DependencyService.Register<MockDataStore>();

            MainPage = new AboutPage();

        }

        protected override void OnStart()
        {
            this.SetCurrentUserFromLocal();


        }

        protected override void OnSleep()
        {
           
        }

        protected override void OnResume()
        {
           
        }

        private async void SetCurrentUserFromLocal()
        {
            // Get local user from local DB - use data to login, if it exists.
            UserLocal userLocal = await LocalUserDatabase.GetCurrentLocalUserAsync();

            if (userLocal != null)
            {
                // Display loading spinner while logging user in
                using (UserDialogs.Instance.Loading("Logging in..."))
                {
                    await DependencyService.Get<IUserService>().LoginUserAsync(userLocal.Email, userLocal.Password);

                    if (DependencyService.Get<IUserService>().IsUserLoggedIn)
                    {
                        Application.Current.MainPage = new DashboardPage();
                    }
                }
            }   
        }


    }
}

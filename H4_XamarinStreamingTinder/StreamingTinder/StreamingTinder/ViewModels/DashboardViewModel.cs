using StreamingTinder.Views;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreamingTinder.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public IUser User { get; private set; }

        public ICommand ClickLogoutButton { get; }

        public DashboardViewModel()
        {
            // Set dependencies.
            this._userService = DependencyService.Get<IUserService>();

            // Check if user is logged in - Redirect if false
            if (!this._userService.IsUserLoggedIn)
                RedirectToLoginPage();

            this.User = this._userService.CurrentUser;

            this.Title ="Home";
            ClickLogoutButton = new Command(LogoutButtonClicked);
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        private async void LogoutButtonClicked()
        {
            bool successfullyLoggedOut = await this._userService.LogoutUserAsync();

            if (successfullyLoggedOut)
            {
                // Delete current user data from local DB
                await App.LocalUserDatabase.EmptyLocalUserRecordsAsync();
                
                // Redirect to login page
                RedirectToLoginPage();
            }
        }

    }
}
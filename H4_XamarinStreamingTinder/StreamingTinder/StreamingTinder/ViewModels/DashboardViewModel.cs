using Acr.UserDialogs;
using StreamingTinder.Views;
using StreamingTinderClassLibrary.Rooms;
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
        private readonly IUserService _userService = DependencyService.Get<IUserService>();
        private readonly IRoomService _roomService = DependencyService.Get<IRoomService>();

        public IUser User { get; private set; }

        private string enterRoomCode;
        public string EnterRoomCode
        {
            get { return enterRoomCode; }
            set
            {
                enterRoomCode = value;
                SetProperty(ref enterRoomCode, value);
            }
        }

        public ICommand ClickLogoutButton { get; }
        public ICommand CreateRoomButton { get; }
        public ICommand EnterRoomButton { get; }

        public DashboardViewModel()
        {
            // Check if user is logged in - Redirect if false
            if (!this._userService.IsUserLoggedIn)
                RedirectToLoginPage();

            this.User = this._userService.CurrentUser;
            this.Title = "Home";

            ClickLogoutButton = new Command(LogoutButtonClicked);
            CreateRoomButton = new Command(CreateRoomButtonClicked);
            EnterRoomButton = new Command(EnterRoomButtonClicked);
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

        /// <summary>
        /// Redirect to create room page.
        /// </summary>
        private async void CreateRoomButtonClicked()
        {
            // Redirect to login page
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateRoomPage(), true);
        }

        /// <summary>
        /// Enter room by room key.
        /// </summary>
        private async void EnterRoomButtonClicked()
        {
            // Redirect to login page
            using (UserDialogs.Instance.Loading("Entering room..."))
            {
                string[] roomKeyErrors = DefaultValidator.ValidRoomKey(this.EnterRoomCode);

                // Check for errors
                if (roomKeyErrors.Length == 0)
                {
                    // No errors
                    // Check if room key mathces
                    try
                    {
                        IRoom matchingRoom = null;
                        matchingRoom = await this._roomService.GetRoomByRoomKeyAsync(this.EnterRoomCode);

                        if (matchingRoom != null)
                        {
                            // A mathing room was found
                            await Application.Current.MainPage.Navigation.PushModalAsync(new RoomPage(matchingRoom), true);
                        }

                        if (matchingRoom == null)
                        {
                            // No mathing room found
                            ShowAlert("A room matching this room key could not be found.");
                        }
                        
                    }
                    catch (Exception e)
                    {
                        // An unexpected error occured, so user could not be created.
                        // Show generic error message to user.
                        ShowAlert("An error occured. Room could not be entered.");
                    }

                }
                else
                {
                    // Errors in user input.
                    // Display errors.
                    string errorMessage = "Invalid room key:" + System.Environment.NewLine;

                    foreach (var error in roomKeyErrors)
                    {
                        errorMessage += System.Environment.NewLine + error;
                    }

                    ShowAlert(errorMessage);
                }
            }
        }
    }
}
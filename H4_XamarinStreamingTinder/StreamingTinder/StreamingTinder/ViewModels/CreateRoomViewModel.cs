using Acr.UserDialogs;
using StreamingTinder.Models;
using StreamingTinder.Views;
using StreamingTinderClassLibrary.Rooms;
using StreamingTinderClassLibrary.StreamingService;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreamingTinder.ViewModels
{
    public class CreateRoomViewModel : BaseViewModel
    {
        private IUserService _userService = DependencyService.Get<IUserService>();
        private IStreamingPlatformService _streamingPlatformService = DependencyService.Get<IStreamingPlatformService>();
        private IRoomService _roomService = DependencyService.Get<IRoomService>();

        public ObservableCollection<HelperModel> StatusRecords { get; set; }
        public ICommand ChangeCommand { protected set; get; }
        ObservableCollection<object> selectedHelperModels;
        public ObservableCollection<object> SelectedHelperModels
        {
            get
            {
                return selectedHelperModels;
            }
            set
            {
                if (selectedHelperModels != value)
                {
                    selectedHelperModels = value;
                    OnPropertyChanged("SelectedHelperModels");
                }
            }
        }

        private string roomName;
        public string RoomName
        {
            get { return roomName; }
            set
            {
                roomName = value;
                SetProperty(ref roomName, value);
            }
        }

        public ICommand ClickCreateButton { get; }

        public CreateRoomViewModel()
        {
            // Check if user is logged in - Redirect if true
            if (!this._userService.IsUserLoggedIn)
                RedirectToLoginPage();

            this.Title = "Create room";

            StatusRecords = new ObservableCollection<HelperModel>();
            SelectedHelperModels = new ObservableCollection<object>();

            // Populates the select list with available streaming platforms.
            PopulateAvailablePlatforms();

            // Commands
            ClickCreateButton = new Command(CreateButtonClicked);
            ChangeCommand = new Command<HelperModel>((key) =>
            {
                if (SelectedHelperModels.Contains(key))
                {
                    SelectedHelperModels.Remove(key);
                }
                else
                {
                    SelectedHelperModels.Add(key);
                }
                key.IsSelected = !key.IsSelected;
            });


        }

        /// <summary>
        /// "Create" - button clicked.
        /// Validates and creates a new room.
        /// Redirect to Dashboard page if successfull.
        /// </summary>
        public async void CreateButtonClicked()
        {
            using (UserDialogs.Instance.Loading("Creating room..."))
            {
                List<IStreamingPlatform> selectedStreamingPlatforms = new List<IStreamingPlatform>();
                foreach (HelperModel selectedItem in this.SelectedHelperModels)
                {
                    selectedStreamingPlatforms.Add(selectedItem.StreamingPlatform);
                }

                // Run validation on input fields.
                string[] roomNameErrors = DefaultValidator.ValidRoomName(this.RoomName);
                bool streamingPlatformsHasBeenSelected = selectedStreamingPlatforms.Count > 0;

                // Check for errors
                if (roomNameErrors.Length == 0 && streamingPlatformsHasBeenSelected)
                {
                    // No errors
                    // Create room
                    try
                    {
                        IUser owner = this._userService.CurrentUser;
                        string roomName = this.RoomName;

                        // Create room
                        await this._roomService.CreateRoomAsync(owner, roomName, selectedStreamingPlatforms);

                        // Redirect to login page
                        RedirectToMainPage();
                        
                    }
                    catch (Exception e)
                    {
                        // An unexpected error occured, so user could not be created.
                        // Show generic error message to user.
                        ShowAlert("An error occured. Room could not be created.");
                    }

                }
                else
                {
                    // Errors in user input.
                    // Display errors.
                    string errorMessage = "Invalid room data:" + System.Environment.NewLine;

                    foreach (var error in roomNameErrors)
                    {
                        errorMessage += System.Environment.NewLine + error;
                    }

                    if (!streamingPlatformsHasBeenSelected)
                    {
                        errorMessage += System.Environment.NewLine + "No streaming service has been selected!";
                    }

                    ShowAlert(errorMessage);
                }
            }
        }

        /// <summary>
        /// Populate available streaming platforms from data source
        /// </summary>
        private async void PopulateAvailablePlatforms()
        {
            List<IStreamingPlatform> streamingPlatformsFromData = await this._streamingPlatformService.GetStreamingPlatformsAsync();

            foreach (var platform in streamingPlatformsFromData)
            {
                StatusRecords.Add(new HelperModel() { IsSelected = false, StreamingPlatform = platform });
            }

        }

    }
}
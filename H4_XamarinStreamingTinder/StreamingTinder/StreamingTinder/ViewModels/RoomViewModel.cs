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
    public class RoomViewModel : BaseViewModel
    {
        private readonly IUserService _userService = DependencyService.Get<IUserService>();
        private readonly IRoomService _roomService = DependencyService.Get<IRoomService>();

        private IRoom selectedRoom;
        public IRoom SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                SetProperty(ref selectedRoom, value);
            }
        }
        public ICommand ShowInfoButton { get; }

        public RoomViewModel() { }

        public RoomViewModel(IRoom room)
        {
            // Check if user is logged in - Redirect if false
            if (!this._userService.IsUserLoggedIn)
                RedirectToLoginPage();

            if (room is null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            this.SelectedRoom = room;
            this.Title = selectedRoom.Name;

            ShowInfoButton = new Command(ShowInfoButtonClicked);
        }

        private async void ShowInfoButtonClicked()
        {
            // Redirect to login page
            await Application.Current.MainPage.Navigation.PushModalAsync(new RoomDetailsPage(SelectedRoom), true);
        }

    }
}
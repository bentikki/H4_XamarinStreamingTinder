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
    public class RoomDetailsViewModel : BaseViewModel
    {
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

        public RoomDetailsViewModel() { }

        public RoomDetailsViewModel(IRoom room)
        {
            if (room is null)
                throw new ArgumentNullException(nameof(room));
            
            this.SelectedRoom = room;
            this.Title = "Test";
        }
    }
}
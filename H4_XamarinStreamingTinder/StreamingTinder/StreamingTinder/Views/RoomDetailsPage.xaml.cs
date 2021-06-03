using StreamingTinder.ViewModels;
using StreamingTinderClassLibrary.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreamingTinder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomDetailsPage : ContentPage
    {
        public RoomDetailsPage(IRoom room)
        {
            InitializeComponent();
            BindingContext = new RoomDetailsViewModel(room);
        }
    }
}
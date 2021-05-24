using StreamingTinder.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace StreamingTinder.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
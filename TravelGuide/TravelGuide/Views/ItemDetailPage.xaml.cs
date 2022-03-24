using System.ComponentModel;
using TravelGuide.ViewModels;
using Xamarin.Forms;

namespace TravelGuide.Views
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
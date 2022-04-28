using System.ComponentModel;
using TravelGuide.ViewModels;
using Xamarin.Forms;

namespace TravelGuide.Views
{
    public partial class LandmarkDetailPage : ContentPage
    {
        public LandmarkDetailPage()
        {
            InitializeComponent();
            BindingContext = new LandmarkDetailViewModel();
        }
    }
}
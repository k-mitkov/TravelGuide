using System;
using System.Collections.Generic;
using TravelGuide.ViewModels;
using TravelGuide.Views;
using Xamarin.Forms;

namespace TravelGuide
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LandmarkDetailPage), typeof(LandmarkDetailPage));
            Routing.RegisterRoute(nameof(NewLandmarkPage), typeof(NewLandmarkPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

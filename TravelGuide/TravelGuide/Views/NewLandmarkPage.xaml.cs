using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravelGuide.Models;
using TravelGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelGuide.Views
{
    public partial class NewLandmarkPage : ContentPage
    {
        public Item Item { get; set; }

        public NewLandmarkPage()
        {
            InitializeComponent();
            BindingContext = new NewLandmarkViewModel();
        }
    }
}
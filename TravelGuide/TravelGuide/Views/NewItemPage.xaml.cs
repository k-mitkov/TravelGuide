using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravelGuide.Models;
using TravelGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelGuide.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
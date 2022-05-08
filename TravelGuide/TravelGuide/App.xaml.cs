using System;
using System.Collections.Generic;
using TravelGuide.Enums;
using TravelGuide.Resources.Themes;
using TravelGuide.Services;
using TravelGuide.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelGuide
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            SetTheme();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// Задава тема на приложението
        /// </summary>
        private void SetTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Current.Resources.MergedDictionaries;
            mergedDictionaries.Clear();
            if (Settings.Settings.Theme == ThemeEnum.LightTheme)
            {
                mergedDictionaries.Add(new LightTheme());
            }
            else
            {
                mergedDictionaries.Add(new DarkTheme());
            }
        }
    }
}

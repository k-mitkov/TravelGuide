using System;
using System.Collections.Generic;
using System.Globalization;
using TravelGuide.Enums;
using TravelGuide.Extensions;
using TravelGuide.Resources.Resx;
using TravelGuide.Resources.Themes;
using TravelGuide.Services;
using TravelGuide.Views;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelGuide
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            SetLanguage();
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
        /// Задава език на приложението
        /// </summary>
        private void SetLanguage()
        {
            LocalizationResourceManager.Current.PropertyChanged += (a, b) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            var cultureName = (Settings.Settings.Lenguage).GetDescription();
            LocalizationResourceManager.Current.CurrentCulture = cultureName == null ? CultureInfo.CurrentCulture : new CultureInfo(cultureName);
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

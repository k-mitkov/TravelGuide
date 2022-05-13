using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TravelGuide.Enums;
using TravelGuide.Extensions;
using TravelGuide.Models;
using TravelGuide.Resources.Themes;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Declarations

        private SupportedLanguage selectedLanguage;
        private IEnumerable<SupportedLanguage> supportedLanguages;

        #endregion

        #region Properties

        /// <summary>
        /// Състояние на бутона
        /// </summary>
        public bool IsThemeButtonToggled
        {
            get
            {
                return Settings.Settings.Theme == ThemeEnum.LightTheme ? true : false;
            }
            set
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                mergedDictionaries.Clear();

                if (value)
                {
                    Settings.Settings.Theme = ThemeEnum.LightTheme;
                    mergedDictionaries.Add(new LightTheme());
                }
                else
                {
                    Settings.Settings.Theme = ThemeEnum.DarkTheme;
                    mergedDictionaries.Add(new DarkTheme());
                }
            }
        }

        /// <summary>
        /// Избран език на програмата
        /// </summary>
        public SupportedLanguage SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;

                    Settings.Settings.Lenguage = value.LanguageType;

                    var langName = value.LanguageType.GetDescription();
                    LocalizationResourceManager.Current.CurrentCulture = langName == null ? CultureInfo.CurrentCulture : new CultureInfo(langName);

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Поддържани от програмата езици
        /// </summary>
        public IEnumerable<SupportedLanguage> SupportedLanguages
        {
            get => supportedLanguages ?? (supportedLanguages = new List<SupportedLanguage>()
            {
                new SupportedLanguage(LanguageEnum.English,"Resources/UnitedKingdom.png"),
                new SupportedLanguage(LanguageEnum.Български,"Resources/Bulgaria.png")
            });
        }

        #endregion

        #region Constructor

        public SettingsViewModel()
        {
            SelectedLanguage = SupportedLanguages.FirstOrDefault(sp => sp.LanguageType == Settings.Settings.Lenguage);
        }

        #endregion
    }
}

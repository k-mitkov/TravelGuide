using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TravelGuide.Enums;
using TravelGuide.Resources.Themes;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Declarations

        private LenguageEnum selectedLanguage;
        private IEnumerable<LenguageEnum> supportedLanguages;

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
        public LenguageEnum SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;

                    Settings.Settings.Lenguage = value;

                    var langName = value.ToString();
                    LocalizationResourceManager.Current.CurrentCulture = langName == null ? CultureInfo.CurrentCulture : new CultureInfo(langName);

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Поддържани от програмата езици
        /// </summary>
        public IEnumerable<LenguageEnum> SupportedLanguages
        {
            get => supportedLanguages ?? (supportedLanguages = new List<LenguageEnum>()
            {
                //new SupportedLanguage(LanguageEnum.English,"Resources/UnitedKingdom.png"),
                //new SupportedLanguage(LanguageEnum.Русский,"Resources/Russia.png"),
                //new SupportedLanguage(LanguageEnum.Български,"Resources/Bulgaria.png")
                LenguageEnum.English,
                LenguageEnum.Български
            });
        }

        #endregion

        #region Constructor

        public SettingsViewModel()
        {
            //SelectedLanguage = SupportedLanguages.FirstOrDefault(sp => sp == Settings.Settings.Lenguage);
            SelectedLanguage = LenguageEnum.English;
        }

        #endregion
    }
}

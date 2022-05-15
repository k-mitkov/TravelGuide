﻿using TravelGuide.Enums;
using Xamarin.Essentials;

namespace TravelGuide.Settings
{
    public static class Settings
    {
        private static Location location;

        public static string LogKey
        {
            get
            {
                return Preferences.Get(nameof(LogKey), null);
            }
            set
            {
                Preferences.Set(nameof(LogKey) , value);
            }
        }

        public static ThemeEnum Theme
        {
            get
            {
                return (ThemeEnum) Preferences.Get(nameof(Theme), (int)ThemeEnum.LightTheme);
            }
            set
            {
                Preferences.Set(nameof(Theme), (int)value);
            }
        }

        public static LanguageEnum Lenguage
        {
            get
            {
                return (LanguageEnum)Preferences.Get(nameof(Lenguage), (int)LanguageEnum.English);
            }
            set
            {
                Preferences.Set(nameof(Lenguage), (int)value);
            }
        }

        public static int LoggedUserId
        {
            get
            {
                return Preferences.Get(nameof(LoggedUserId), -1);
            }
            set
            {
                Preferences.Set(nameof(LoggedUserId), value);
            }
        }

        public static string LoggedUser
        {
            get
            {
                return Preferences.Get(nameof(LoggedUser), "Admin");
            }
            set
            {
                Preferences.Set(nameof(LoggedUser), value);
            }
        }

        public static Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TravelGuide.Resources.Resx;
using Xamarin.CommunityToolkit.Helpers;

namespace TravelGuide.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// Връща описанието на enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        /// <summary>
        /// Връща описанието на enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DisplayAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        /// <summary>
        /// Връща описанието от речника
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetLocalisedResourceByDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (enumValue.ToString() == null)
                return null;

            return AppResources.ResourceManager.GetString(enumValue.GetDescription(), LocalizationResourceManager.Current.CurrentCulture);
        }

        
    }
}

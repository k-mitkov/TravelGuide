using System;
using System.Globalization;
using TravelGuide.Resources.Resx;
using Xamarin.Forms;

namespace TravelGuide.Converters
{
    public class DistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (double)value == double.MaxValue)
            {
                return "";
            }
            double distance = (double)value;
            return Math.Round(distance, 2) + AppResources.ResourceManager.GetString("strKm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

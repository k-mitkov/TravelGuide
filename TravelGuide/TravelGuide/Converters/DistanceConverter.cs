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
            if (value == null)
            {
                return "";
            }
            double distance = (double)value;
            return distance + AppResources.ResourceManager.GetString("strKm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

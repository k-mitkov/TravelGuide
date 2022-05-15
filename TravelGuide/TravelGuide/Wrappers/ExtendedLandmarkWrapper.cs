
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelGuide.ClassLibrary.Models;
using Xamarin.Forms;

namespace TravelGuide.Wrappers
{
    public class ExtendedLandmarkWrapper
    {
        private LandmarkWrapper landmark;

        public ExtendedLandmarkWrapper(LandmarkWrapper landmark)
        {
            this.landmark = landmark;
            Distance = Calculate();
        }

        #region Properties

        public int? Id => landmark.Landmark.Id;

        public string Name => Settings.Settings.Lenguage == Enums.LanguageEnum.Български ? landmark.Landmark.Name1 : landmark.Landmark.Name2;

        public double Distance { get; set; }

        public decimal Latitude
        {
            get
            {
                return landmark.Landmark.Latitude;
            }
            set
            {
                landmark.Landmark.Latitude = value;
            }
        }

        public decimal Longitude
        {
            get
            {
                return landmark.Landmark.Longitude;
            }
            set
            {
                landmark.Landmark.Longitude = value;
            }
        }

        public string Description => Settings.Settings.Lenguage == Enums.LanguageEnum.Български ? landmark.Landmark.Description1 : landmark.Landmark.Description2;

        public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(landmark.Image));

        public LandmarkWrapper landmarkwrapper => landmark;

        #endregion

        private double Calculate()
        {
            if(Latitude == -1 || Longitude == -1 || Settings.Settings.Location == null || Settings.Settings.Location.Latitude == -1 || Settings.Settings.Location.Longitude == -1)
            {
                return double.MaxValue;
            }
            Latitude /= 10000000000000;
            Longitude /= 10000000000000;

            var radiansOverDegrees = (Math.PI / 180.0);

            var sLatitudeRadians = (double)Latitude * radiansOverDegrees;
            var sLongitudeRadians = (double)Longitude * radiansOverDegrees;
            var eLatitudeRadians = Settings.Settings.Location.Latitude * radiansOverDegrees;
            var eLongitudeRadians = Settings.Settings.Location.Longitude * radiansOverDegrees;

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Using 3956 as the number of miles around the earth
            var result2 = 3956.0 * 2.0 *
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return result2;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using TravelGuide.ClassLibrary.Models;

namespace TravelGuide.Wrappers
{
    public class ExtendedLandmarkWrapper
    {
        private LandmarkWrapper landmark;

        public ExtendedLandmarkWrapper(LandmarkWrapper landmark)
        {
            this.landmark = landmark;
        }

        #region Properties

        public int? Id => landmark.Landmark.Id;

        public string Name => Settings.Settings.Lenguage == Enums.LanguageEnum.Български ? landmark.Landmark.Name1 : landmark.Landmark.Name2;

        public double Distance { get; set; }

        public decimal Latitude => landmark.Landmark.Latitude;

        public decimal Longitude => landmark.Landmark.Longitude;

        public string Description => Settings.Settings.Lenguage == Enums.LanguageEnum.Български ? landmark.Landmark.Description1 : landmark.Landmark.Description2;

        //public Bitmap Image => landmark.Image;

        public string Image { get; set; }


        #endregion
    }
}

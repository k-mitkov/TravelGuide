using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelGuide.Wrappers
{
    public class LandmarkWrapper
    {
        #region Properties

        public int? Id { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public Bitmap Image { get; set; }


        #endregion
    }
}

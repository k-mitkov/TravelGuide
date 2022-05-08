using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelGuide.Services;
using TravelGuide.Wrappers;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class MainViewModel
    {
        #region Declarations

        private MockDataStore data;

        #endregion


        #region Properties

        public List<LandmarkWrapper> Landmars => data.GetItemsAsync().Result.ToList();

        public ImageSource Image2
        {
            get
            {
                ImageSourceConverter c = new ImageSourceConverter();
                return (ImageSource)c.ConvertFrom(Landmars.FirstOrDefault().Image);
            }
        }


        #endregion

            #region Constructors


        public MainViewModel()
        {
            data = new MockDataStore();
        }

        #endregion
    }
}

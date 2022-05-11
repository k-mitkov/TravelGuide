using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Services;
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
                return null;//(ImageSource)c.ConvertFrom(Landmars.FirstOrDefault().Image);
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

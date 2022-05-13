using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Services;
using TravelGuide.Views;
using TravelGuide.Wrappers;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Declarations

        private MockDataStore data;

        private ExtendedLandmarkWrapper selectedLandmars;

        #endregion


        #region Properties

        public ExtendedLandmarkWrapper SelectedLandmars
        {
            get
            {
                return selectedLandmars;
            }
            set
            {
                selectedLandmars = null;
                OnPropertyChanged();

                OnItemSelected(value);
            }
        }

        public List<ExtendedLandmarkWrapper> Landmars
        {
            get
            {
                var a = data.GetItemsAsync().Result.ToList();
                a[0].Distance = 4.5;
                a[1].Distance = 6.7;
                a[2].Distance = 11.4;
                a[3].Distance = 15.9;
                a[4].Distance = 19.8;
                a[5].Distance = 35.2;
                a[6].Distance = 45.7;
                a[7].Distance = 25.6;

                a[0].Image = "Resources/a.jpg";
                a[1].Image = "Resources/s.jpg";
                a[2].Image = "Resources/d.jpg";
                a[3].Image = "Resources/f.jpg";
                a[4].Image = "Resources/g.jpg";
                a[5].Image = "Resources/h.jpg";
                a[6].Image = "Resources/j.jpg";
                a[7].Image = "Resources/a.jpg";
                return a;
            }
        }

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

        #region Methods

        private async void OnItemSelected(ExtendedLandmarkWrapper item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(LandmarkDetailPage)}?{nameof(LandmarkDetailViewModel.ItemId)}={item.Id}");
        }

        #endregion
    }
}

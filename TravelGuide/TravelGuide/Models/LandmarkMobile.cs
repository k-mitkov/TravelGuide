using System.IO;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Helpers;
using Xamarin.Forms;

namespace TravelGuide.Models
{
    public class LandmarkMobile
    {
        #region Declarations

        private LandmarkWrapper landmark;
        private ImageSource imagePath;

        #endregion

        #region Constructor

        public LandmarkMobile(LandmarkWrapper landmark)
        {
            Landmark = landmark;
        }

        #endregion

        #region Properties

        public LandmarkWrapper Landmark
        {
            get => landmark;

            set => landmark = value;
        }

        public ImageSource ImagePath
        {
            get => imagePath ?? (imagePath = ImageHelper.GetImageSource(landmark.Image));
        }

        #endregion
    }
}

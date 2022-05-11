using System.IO;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Helpers;
using Xamarin.Forms;

namespace TravelGuide.Models
{
    public class UserMobile
    {
        #region Declarations

        private UserWrapper user;
        private ImageSource imagePath;

        #endregion

        #region Constructor

        public UserMobile(UserWrapper user)
        {
            User = user;
        }

        #endregion

        #region Properties

        public UserWrapper User
        {
            get => user;

            set => user = value;
        }

        public ImageSource ImagePath
        {
            get => imagePath ?? (imagePath = ImageHelper.GetImageSource(user.Image));
        }

        #endregion
    }
}

using TravelGuide.Database.Entities;

namespace TravelGuide.ClassLibrary.Models
{
    public class UserWrapper
    {
        #region Declarations

        private User user;
        private byte[] image;

        #endregion

        #region Constructor

        public UserWrapper(User user, byte[] image)
        {
            User = user;
            Image = image;
        }

        public UserWrapper()
        {

        }

        #endregion

        #region Properties

        public User User
        {
            get => user;

            set => user = value;
        }

        public byte[] Image
        {
            get => image;

            set => image = value;
        }

        #endregion
    }
}

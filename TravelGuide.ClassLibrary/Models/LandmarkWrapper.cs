using TravelGuide.Database.Entities;

namespace TravelGuide.ClassLibrary.Models
{
    public class LandmarkWrapper
    {
        #region Declarations

        private Landmark landmark;
        private byte[] image;

        #endregion

        #region Constructor

        public LandmarkWrapper(Landmark landmark, byte[] image)
        {
            Landmark = landmark;
            Image = image;
        }

        public LandmarkWrapper()
        {

        }

        #endregion

        #region Properties

        public Landmark Landmark
        {
            get => landmark;

            set => landmark = value;
        }
        
        public byte[] Image
        {
            get => image;

            set => image = value;
        }

        #endregion
    }
}

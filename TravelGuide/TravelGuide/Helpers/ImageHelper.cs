using System.IO;
using Xamarin.Forms;

namespace TravelGuide.Helpers
{
    public static class ImageHelper
    {

        public static ImageSource GetImageSource(byte[] image)
        {
            MemoryStream mem = new MemoryStream(image);

            return ImageSource.FromStream(() => mem);
        }

        public static byte[] GetImage(Stream stream)
        {
            using (var mem = new MemoryStream())
            {
                stream.CopyTo(mem);
                return mem.ToArray();
            }
        }
    }
}

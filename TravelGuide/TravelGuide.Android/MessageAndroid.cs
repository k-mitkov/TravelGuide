using Android.App;
using Android.Widget;
using TravelGuide.Droid;
using TravelGuide.Intefaces;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace TravelGuide.Droid
{
    public class MessageAndroid : IMessage
    {
        private Toast toast;

        public void LongAlert(string message)
        {
            toast?.Cancel();

            toast = Toast.MakeText(Application.Context, message, ToastLength.Long);

            toast.Show();
        }

        public void ShortAlert(string message)
        {
            toast?.Cancel();

            toast = Toast.MakeText(Application.Context, message, ToastLength.Short);

            toast.Show();
        }
    }
}
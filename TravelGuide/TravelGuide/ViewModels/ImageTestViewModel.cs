using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class ImageTestViewModel : BaseViewModel
    {
        private ImageSource imagePath;

        public ImageTestViewModel()
        {
            Task.Run(async () =>await GetImage());
        }

        public ImageSource ImagePath
        {
            get => imagePath;

            set => SetProperty(ref imagePath, value);
        }

        private async Task GetImage()
        {
            try
            {
                HttpClient client;
                MediaTypeWithQualityHeaderValue mediaTypeJson;
                HttpClientHandler clientHandler;

                clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                //За да работи с обекти в json формат
                mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

                client = new HttpClient(clientHandler);

                // За избиране на твой адрес виж Program в TravelGuide.WebApi проекта. Също и connectionString-ът в проекта с базата трябва да промениш.
                //Вместо landmark, може да са user и comment, за да се извикат другите контролери от API-ът.
                client.BaseAddress = new Uri("https://192.168.0.102:5001/api/landmark/");
                client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

                var response3 = await client.GetAsync("GetImageById/" + 5.ToString());

                if (response3.IsSuccessStatusCode)
                {
                    var bytes = await response3.Content.ReadAsAsync<byte[]>();

                    MemoryStream mem = new MemoryStream(bytes);

                    ImagePath = ImageSource.FromStream(() => mem);
                }
            }
            catch(Exception ex)
            {

            }
        }

    }
}

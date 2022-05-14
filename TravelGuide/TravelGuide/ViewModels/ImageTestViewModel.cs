using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class ImageTestViewModel : BaseViewModel
    {
        private Command createLandmarkCommand;
        private Command registerCommand;
        private ImageSource imagePath;

        public ImageTestViewModel()
        {
            Task.Run(async () => await GetLandmark());
        }

        public ImageSource ImagePath
        {
            get => imagePath;

            set => SetProperty(ref imagePath, value);
        }

        public Command CreateLandmarkCommand => createLandmarkCommand ?? (createLandmarkCommand = new Command(async () => await CreateLandmark()));
        public Command RegisterCommand => registerCommand ?? (registerCommand = new Command(async () => await Register()));

        private async Task CreateLandmark()
        {
            FileResult img = await MediaPicker.PickPhotoAsync();

            Stream stream = await img.OpenReadAsync();

            var imageBytes = ImageHelper.GetImage(stream);

            var landmark = new LandmarkWrapper()
            {
                Landmark = new Landmark()
                {
                    Name1 = "proba",
                    Name2 = "проба",
                    Description1 = "proba",
                    Description2 = "проба",
                    Latitude = 10,
                    Longitude = 10
                },
                Image = imageBytes
            };

            if (stream != null)
            {
                ImagePath = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }

            HttpClient client;
            MediaTypeWithQualityHeaderValue mediaTypeJson;
            HttpClientHandler clientHandler;

            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://192.168.0.102:5001/api/landmark/");
            client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

            var response = await client.PostAsJsonAsync("Post", landmark);
        }

        private async Task Register()
        {
            var user = new User()
            {
                Username = "proba2",
                Password = "proba2",
                Email = "proba2@abv.bg",

            };
       
            HttpClient client;
            MediaTypeWithQualityHeaderValue mediaTypeJson;
            HttpClientHandler clientHandler;

            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://192.168.0.102:5001/api/user/");
            client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

            var response = await client.PostAsJsonAsync("Register", user);

            if (response.IsSuccessStatusCode)
            {
                var isSuccessfull = await response.Content.ReadAsAsync<bool>();
            }
        }

        private async Task GetLandmark()
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

                var response3 = await client.GetAsync("GetAll");

                if (response3.IsSuccessStatusCode)
                {
                    var landmarks = await response3.Content.ReadAsAsync<List<LandmarkWrapper>>();

                    MemoryStream mem = new MemoryStream(landmarks[landmarks.Count - 1].Image);

                    ImagePath = ImageSource.FromStream(() => mem);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}

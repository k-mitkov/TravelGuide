using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.Helpers;
using TravelGuide.Intefaces;
using TravelGuide.Models;
using TravelGuide.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class NewLandmarkViewModel : BaseViewModel
    {
        private string name;
        private string description;
        private ImageSource uplodedImage;
        private byte[] imageBytes;
        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        private ICommand onAppearingCommand;

        public NewLandmarkViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            UploadCommand = new Command(async () => await OnUpload());
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public ImageSource UplodedImage
        {
            get
            {
                return uplodedImage;
            }
            set
            {
                uplodedImage = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand { get; }
        public Command UploadCommand { get; }

        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        public ICommand OnAppearingCommand => onAppearingCommand ?? (onAppearingCommand = new Command(OnAppearing));

        /// <summary>
        /// Презарежда потребителите при всяко отваряне на екрана
        /// </summary>
        /// <param name="obj"></param>
        private void OnAppearing(object obj)
        {
            Name = null;
            Description = null;
        }

        private async Task OnUpload()
        {
            FileResult img = await MediaPicker.PickPhotoAsync();

            Stream stream = await img.OpenReadAsync();

            imageBytes = ImageHelper.GetImage(stream);
            UplodedImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        private async void OnSave()
        {

            Task.Run(async () => await CreateLandmark());

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private async Task CreateLandmark()
        {
            byte[] imageBytes = this.imageBytes;


            var landmark = new LandmarkWrapper()
            {
                Landmark = new Landmark()
                {
                    Name1 = Name,
                    Name2 = Name,
                    Description1 = Description,
                    Description2 = Description,
                    Latitude = (decimal)(Settings.Settings.Location != null ? Settings.Settings.Location.Latitude : -1),
                    Longitude = (decimal)(Settings.Settings.Location != null ? Settings.Settings.Location.Longitude : -1)
                },
                Image = imageBytes
            };

            HttpClient client;
            MediaTypeWithQualityHeaderValue mediaTypeJson;
            HttpClientHandler clientHandler;

            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(AppConstands.Url + "/api/landmark/");
            client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

            var response = await client.PostAsJsonAsync("Post", landmark);
        }
    }
}

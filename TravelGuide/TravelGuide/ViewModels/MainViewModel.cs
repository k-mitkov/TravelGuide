using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Converters;
using TravelGuide.Intefaces;
using TravelGuide.Resources.Resx;
using TravelGuide.Services;
using TravelGuide.Views;
using TravelGuide.Wrappers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Declarations

        private MockDataStore data;

        private ExtendedLandmarkWrapper selectedLandmars;

        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        private ICommand onAppearingCommand;

        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        private ICommand onDisappearinggCommand;

        List<ExtendedLandmarkWrapper> landmarks;

        #endregion

        #region Properties

        public ExtendedLandmarkWrapper SelectedLandmark
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

        public List<ExtendedLandmarkWrapper> Landmarks
        {
            get
            {
                //var a = data.GetItemsAsync().Result.ToList();
                //a[0].Distance = 4.5;
                //a[1].Distance = 6.7;
                //a[2].Distance = 11.4;
                //a[3].Distance = 15.9;
                //a[4].Distance = 19.8;
                //a[5].Distance = 35.2;
                //a[6].Distance = 45.7;
                //a[7].Distance = 25.6;

                //a[0].Image = "Resources/a.jpg";
                //a[1].Image = "Resources/s.jpg";
                //a[2].Image = "Resources/d.jpg";
                //a[3].Image = "Resources/f.jpg";
                //a[4].Image = "Resources/g.jpg";
                //a[5].Image = "Resources/h.jpg";
                //a[6].Image = "Resources/j.jpg";
                //a[7].Image = "Resources/a.jpg";
                //return a;
                return landmarks;
            }
            set
            {
                landmarks = value;
                OnPropertyChanged();
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

        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        public ICommand OnAppearingCommand => onAppearingCommand ?? (onAppearingCommand = new Command(OnAppearing));

        #endregion

        #region Constructors


        public MainViewModel()
        {
            
        }

        #endregion

        #region Methods

        private async void OnItemSelected(ExtendedLandmarkWrapper item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LandmarkDetailPage)}?{nameof(LandmarkDetailViewModel.ItemId)}={item.Id}");
        }


        /// <summary>
        /// Презарежда потребителите при всяко отваряне на екрана
        /// </summary>
        /// <param name="obj"></param>
        private void OnAppearing(object obj)
        {
            IsBusy = true;
            Task.Run(async () => await GetLandmark());
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
                client.BaseAddress = new Uri(AppConstands.Url + "/api/landmark/");
                client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

                var response3 = await client.GetAsync("GetAll");

                if (response3.IsSuccessStatusCode)
                {
                    var tempLandmarks = await response3.Content.ReadAsAsync<List<LandmarkWrapper>>();

                    Landmarks = tempLandmarks.Select(t => new ExtendedLandmarkWrapper(t)).ToList();
                    Landmarks.Sort((x, y) => x.Distance.CompareTo(y.Distance));
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Resolve<IMessage>().LongAlert(AppResources.strNoConnection);
                });
            }
        }

        #endregion
    }
}

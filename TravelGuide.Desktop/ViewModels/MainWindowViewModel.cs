using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;
using TravelGuide.Database.Repositories;
using TravelGuide.Desktop.Models;

namespace TravelGuide.Desktop.ViewModels
{
    public class MainWindowViewModel
    {
        #region Declarations

        private ObservableCollection<LandmarkWrapper> landmarks;
        private RepositoryManager repositoryManager;

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            repositoryManager = new RepositoryManager();
            Landmarks = new ObservableCollection<LandmarkWrapper>();
            Task.Run(() => StartListening());
        }

        #endregion

        #region Properties

        public ObservableCollection<LandmarkWrapper> Landmarks
        {
            get => landmarks;

            set => landmarks = value;
        }

        #endregion

        #region Methods

        private void StartListening()
        {
            HttpListener listener = new HttpListener();
          
            listener.Prefixes.Add("http://localhost:5002/");

            try
            {
                listener.Start();
            }
            catch (HttpListenerException hlex)
            {
                return;
            }
            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                using (var reader = new StreamReader(request.InputStream,
                                                     request.ContentEncoding))
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        var landmark = JsonConvert.DeserializeObject<TravelGuide.ClassLibrary.Models.LandmarkWrapper>(reader.ReadToEnd());
                        Landmarks.Add(new LandmarkWrapper(landmark.Landmark, AddLandmark, CancelLandmark));

                        HttpListenerResponse response = context.Response;
                        string responseString = "Send successfully";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        
                        response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                    });
                }
            }
            listener.Close();
        }

        private void CancelLandmark(LandmarkWrapper landmark)
        {
            Landmarks.Remove(landmark);
        }

        private void AddLandmark(LandmarkWrapper landmark)
        {
            repositoryManager.LandmarksRepository.Insert(landmark.Landmark);
            Landmarks.Remove(landmark);
        }

        #endregion
    }
}


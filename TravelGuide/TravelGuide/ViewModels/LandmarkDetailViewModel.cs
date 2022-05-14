using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.Models;
using TravelGuide.Services;
using TravelGuide.Wrappers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class LandmarkDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string newComment;
        ExtendedLandmarkWrapper landmark;
        List<Models.Comment> comments;

        public LandmarkDetailViewModel()
        {
            AddCommentCommand = new Command(async () => await AddComment());
            comments = new List<Models.Comment> { new Models.Comment { Writer = "vasil.lazarov", CommentText = "Супер е!" },
                    new Models.Comment { Writer = "krasimir.mitkov", CommentText = "Златните мостове е едно от красивите места за отдих около София." },
                    new Models.Comment { Writer = "tihomir.nikolov", CommentText = "Местността е чудесно начало за множество разходки сред природата или за пикник в събота и неделя." },
                    new Models.Comment { Writer = "aleksander.blagoev", CommentText = "Хареса ми." } };
        }

        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string NewComment
        {
            get => newComment;
            set 
            {
                newComment = value;
                OnPropertyChanged();
            }
        }

        public string ItemId
        {
            set
            {
                itemId = value;
                IsBusy = true;
                Task.Run(async () => await GetLandmark(int.Parse(itemId)));
            }
        }

        public ExtendedLandmarkWrapper Landmark
        {
            get
            {
                return landmark;
            }
            set
            {
                landmark = value;
                OnPropertyChanged();
            }
        }

        public List<Models.Comment> Comments
        {
            get
            {
                return comments;
            }
        }

        public Command AddCommentCommand { get; set; }

        private async Task GetLandmark(int id)
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

                /*Expression, който се използва за филтриране. Пише се в стринг понеже Expression-а не може да се сериализира и десериализира. Затова се прави направо в
            API от този стринг. Индексът винаги трябва да е i, за да се построи правилно в API. Може да се промени в заявката в API-ът.*/
                string query = "i.Id == " + id;

                //Тук се вика името на HTTP...(име) пътят от API контролера.
                var response = await client.GetAsync("GetMultipleByCondition/" + query);

                //Пример за четене на резултат от заявката от API-ът.

                //Чистите заявки към базата не са имплементирани в API-ът. Засега не знам как ще станат ако трябват, но не мисля че ще трябват.
                if (response.IsSuccessStatusCode)
                {
                    var landmarks = await response.Content.ReadAsAsync<List<Landmark>>();
                    Landmark = new ExtendedLandmarkWrapper(new LandmarkWrapper(landmarks.FirstOrDefault(), null));
                    IsBusy = false;
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        private async Task AddComment()
        {

            Database.Entities.Comment comment = new Database.Entities.Comment() { Content = NewComment, UserId = Settings.Settings.LoggedUserId, LandmarkId = Landmark.Id.Value};


            HttpClient client;
            MediaTypeWithQualityHeaderValue mediaTypeJson;
            HttpClientHandler clientHandler;

            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(AppConstands.Url + "/api/comment/");
            client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

            var response = await client.PostAsJsonAsync("Post", comment);

            if (response.IsSuccessStatusCode)
            {
                NewComment = "";
                Comments.Add(new Models.Comment() { CommentText = comment.Content, Writer = "krasimir.mitkov" });
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnPropertyChanged(nameof(Comments));
                }
                );
            }
        }

    }
}

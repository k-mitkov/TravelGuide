using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace DBTest
{
    class Program
    {
        //И трите класа трябват за да се осъществи връзка
        static HttpClient client;
        static MediaTypeWithQualityHeaderValue mediaTypeJson;
        static HttpClientHandler clientHandler;
        static ImageConverter imageConverter = new ImageConverter();

        static void Main(string[] args)
        {
            //Този клас се ползва за премахване на изискването за SSL сертификат, понеже сайтът е хостнат локално в мержата, а не е localhost.
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //За да работи с обекти в json формат
            mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client = new HttpClient(clientHandler);

            // За избиране на твой адрес виж Program в TravelGuide.WebApi проекта. Също и connectionString-ът в проекта с базата трябва да промениш.
            //Вместо landmark, може да са user и comment, за да се извикат другите контролери от API-ът.
            client.BaseAddress = new Uri("https://192.168.0.102:5001/api/landmark/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(mediaTypeJson);

            Task.Run(async () => await Get()).Wait();
        }

        static async Task Get()
        {
            /*Expression, който се използва за филтриране. Пише се в стринг понеже Expression-а не може да се сериализира и десериализира. Затова се прави направо в
            API от този стринг. Индексът винаги трябва да е i, за да се построи правилно в API. Може да се промени в заявката в API-ът.*/
            string query = "i.Id == 1";

            //Тук се вика името на HTTP...(име) пътят от API контролера.
            var response = await client.GetAsync("GetMultipleByCondition/" + query);

            //Пример за четене на резултат от заявката от API-ът.

            //Чистите заявки към базата не са имплементирани в API-ът. Засега не знам как ще станат ако трябват, но не мисля че ще трябват.
            if (response.IsSuccessStatusCode)
            {
                var landmarks = await response.Content.ReadAsAsync<List<Landmark>>();
            }

            Landmark landmark = new Landmark()
            {
                Name1 = "проба",
                Name2 = "test",
                Description1 = "Описание01234567890123456789",
                Description2 = "Descrpition01234567890123456789",
                Longitude = 20.2m,
                Latitude = 15.5m,
            };

            //Пример как се записва в базата.
            //Изпраща заявка за създаване на забележителност. Трябва да са включени и API-ът и desktop приложението. Иначе няма да мине заявката.
            var response2 = await client.PostAsJsonAsync("Post", landmark);

            if (response2.IsSuccessStatusCode)
            {

            }

            //Пример как се чете и се визуализира картинка съм добавил в TravelGuide ImageTestPage и ImageTestViewModel.
            var response3 = await client.GetAsync("GetImageById/" + 5.ToString());

            if (response3.IsSuccessStatusCode)
            {
                var bytes = await response3.Content.ReadAsAsync<byte[]>();
                Bitmap bm = (Bitmap)imageConverter.ConvertFrom(bytes);
            }

        }
    }
}


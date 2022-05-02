using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace DBTest
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private static MediaTypeWithQualityHeaderValue _mediaTypeJson;


        static void Main(string[] args)
        {

            _mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

            client.BaseAddress = new Uri("https://localhost:5001/api/user/");
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(_mediaTypeJson);

            Task.Run(async () => await Get()).Wait();

        }

        static async Task Get()
        {
            string query = "i.Id == 1";

            var response = await client.GetAsync("GetByCondition/" + query);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<List<User>>();
            }          
        }
    }
}


using Android;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TravelGuide.Models;
using TravelGuide.Wrappers;
using Xamarin.Forms;

namespace TravelGuide.Services
{
    public class MockDataStore : IDataStore<LandmarkWrapper>
    {
        readonly List<LandmarkWrapper> items;

        public MockDataStore()
        {
            try
            {
                Android.Graphics.Bitmap bitmap;
                var key = "icon_about";
                using (var imageStream = Assembly.GetAssembly(typeof(TravelGuide.App)).GetManifestResourceStream(key))
                {
                    bitmap = BitmapFactory.DecodeStream(imageStream);
                }
                items = new List<LandmarkWrapper>()
            {
                new LandmarkWrapper { Id = 1, Name1 = "TU" , Description1 = "TU-Sofia is sdasdsadsadsadasdsad", Image = bitmap},
                new LandmarkWrapper { Id = 1, Name1 = "TU" , Description1 = "TU-Sofia is sdasdsadsadsadasdsad", Image = bitmap},
                new LandmarkWrapper { Id = 1, Name1 = "TU" , Description1 = "TU-Sofia is sdasdsadsadsadasdsad", Image = bitmap},
                new LandmarkWrapper { Id = 1, Name1 = "TU" , Description1 = "TU-Sofia is sdasdsadsadsadasdsad", Image = bitmap},
                new LandmarkWrapper { Id = 1, Name1 = "TU" , Description1 = "TU-Sofia is sdasdsadsadsadasdsad", Image = bitmap}
            };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task<bool> AddItemAsync(LandmarkWrapper item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(LandmarkWrapper item)
        {
            var oldItem = items.Where((LandmarkWrapper arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((LandmarkWrapper arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<LandmarkWrapper> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<LandmarkWrapper>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
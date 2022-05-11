using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Models;
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
            var oldItem = items.Where((LandmarkWrapper arg) => arg.Landmark.Id == item.Landmark.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((LandmarkWrapper arg) => arg.Landmark.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<LandmarkWrapper> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Landmark.Id.ToString() == id));
        }

        public async Task<IEnumerable<LandmarkWrapper>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
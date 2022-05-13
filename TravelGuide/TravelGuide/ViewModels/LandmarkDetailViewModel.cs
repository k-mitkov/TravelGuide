using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TravelGuide.Models;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class LandmarkDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public List<Comment> Comments
        {
            get
            {
                return new List<Comment> { new Comment { Writer = "vasil.lazarov", CommentText = "Супер е!" },
                    new Comment { Writer = "krasimir.mitkov", CommentText = "Златните мостове е едно от красивите места за отдих около София." },
                    new Comment { Writer = "tihomir.nikolov", CommentText = "Местността е чудесно начало за множество разходки сред природата или за пикник в събота и неделя." },
                    new Comment { Writer = "aleksander.blagoev", CommentText = "Хареса ми." }
                };

            }
        }
    }
}

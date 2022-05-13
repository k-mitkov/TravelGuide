using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.Models;
using TravelGuide.Wrappers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace TravelGuide.Services
{
    public class MockDataStore : IDataStore<ExtendedLandmarkWrapper>
    {
        readonly List<ExtendedLandmarkWrapper> items;

        public MockDataStore()
        {
            try
            {

                items = new List<ExtendedLandmarkWrapper>()
            {
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{ Id = 1, Name1 = "Боянски Водопад", Description1 = "Този красив водопад се намира край София – в горите на Витоша и е само на 8 км. от столицата и на 5 км. от Бояна.Мястото е разкошно както за разходка през лятото, така и през зимата.Но имайте предвид, че пътят до него на места е доста стръмен, за това бъдете внимателни."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 2, Name1 = "Златните Мостове" , Description1 = "Златните мостове е един от популярните планински туристически обекти и красиви места за отдих около София. Златни мостове се намират в природен парк Витоша в средното течение на Владайска река и е само на около 7 км от София. Впечатляващи са огромните овални морени по коритото на реката. Местността е чудесно начало за множество разходки сред природата или за пикник в събота и неделя."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 3, Name1 = "Панчаревското Езеро" , Description1 = "Панчаревското езеро е най-голямата водна площ в близост до София и в момента е топ място за отмора и забавление на софиянци. Разположено е на 15 км от центъра на града, посока Боровец. Тук ще намерите 3 рекреационни центъра – СПА комплекс около минералните извори, спортен център на гребна база „Средец“ и зона за отдих и атракции в местността „Плажа“."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 4, Name1 = "Черни Връх" , Description1 = "Изкачването до него не е за всеки, но все пак е сред най-известните природни забележителности около София. Това е най-високия връх на Витоша с надморска височина 2290 м. От десетилетия изкачването на Черни връх е традиция за много софиянци. Най-популярния маршрут до него минава през известната хижа Алеко. Чувството когато сте на него и гледате красотата наоколо от високо, е неописуемо."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 5, Name1 = "Манастир „Свети Спас“" , Description1 = "Манастирът „Свети Спас“ е построен по време на Второто българско царство. Разположен е на около 20-25 км. от центъра на София в склоновете на Лозенска планина, на 4 км. от село Лозен. Тази света обител е богата на образи на светци и на исторически личности, което я прави една от наистина интересните забележителности около София. От тук има невероятна гледка към Софийското поле."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 5, Name1 = "Водопад Котлите" , Description1 = "Boдoпaд Koтлитe е разположен близо дo гpад Гoдeч при c. Tyдeн на 55 км от София. Намира се нa peĸa Дpaĸyл в много живописна местност. Пътят през него минава покрай Букоровския манастир и самият водопад е на около 200 метра от него. Водопадът е много красив но имайте предвид, че се намира в стръмно дере и слизането до котела под водопада е по една доста стръмна пътечка сред скали."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 5, Name1 = "Лакатнишките Скали" , Description1 = "Само на 60 км. от столицата, тези чудати скали изградени от пясъчници и варовици, са едно от най-интересните и красиви места за разходка около София. Намират се на левия склон на Искърския пролом в Стара планина. Височината им на места достига до над 250 метра и се разпростират уникално над водите на река Искър."}, Image = null }),
                new ExtendedLandmarkWrapper( new LandmarkWrapper { Landmark = new Landmark{Id = 5, Name1 = "FDGDFGFDGDFG" , Description1 = "FDGDFGFDGDFG is sdasdsadsadsadasdsad"}, Image = null })
            };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task<bool> AddItemAsync(ExtendedLandmarkWrapper item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ExtendedLandmarkWrapper item)
        {
            var oldItem = items.Where((ExtendedLandmarkWrapper arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ExtendedLandmarkWrapper arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ExtendedLandmarkWrapper> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<ExtendedLandmarkWrapper>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
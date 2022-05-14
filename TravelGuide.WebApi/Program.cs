using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TravelGuide.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Проекта се билдва като TravelGuide.WebApi
                    //тук може да си смениш към твои адреси да са заявките, не е задължително да са тези. После от мобилното приложение се обръщаш към тях някой от тях
                    webBuilder
                    .UseUrls("http://192.168.1.3:5000", "https://192.168.1.3:5001")
                    .UseStartup<Startup>();
                });
    }
}

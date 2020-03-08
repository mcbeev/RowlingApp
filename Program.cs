using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace RowlingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         /* 1. Default out of the box implementation for Blazor server for CreateDefaultBuilder
          */
         //Host.CreateDefaultBuilder(args)
         //    .ConfigureWebHostDefaults(webBuilder =>
         //    {
         //        webBuilder.UseStartup<Startup>();
         //    });


         /* 2. Adding in Azure App Configuration and Feature Flags to our Builder
          *     ensure your app. Uses default options (caches every 30 seconds)
          *     Make sure you have ConnectionStrings:AppConfig in your user's secrets.json and in Azure App Service AppSettings 
          */
         //Host.CreateDefaultBuilder(args)
         //   .ConfigureWebHostDefaults(webBuilder =>
         //    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
         //    {
         //       var settings = config.Build();
         //       config.AddAzureAppConfiguration(options => {
         //           options.Connect(settings["ConnectionStrings:AppConfig"])
         //                  .UseFeatureFlags();
         //       });
         //    })
         //    .UseStartup<Startup>());


         /* 3. Same as above but with label filter and custom cache expiration time (set to every 15 seconds).
          *     The label must matche the running environment's environment variable 
          *     (same as env.IsDevelopment() or env.IsProduction())
          */
         Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    config.AddAzureAppConfiguration(options =>
                    {
                        options.Connect(settings["ConnectionStrings:AppConfig"])
                            .UseFeatureFlags(opt => {
                                opt.CacheExpirationTime = TimeSpan.FromSeconds(15); 
                                opt.Label = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();
                            }); 
                    });
                })
                .UseStartup<Startup>());

    }
}

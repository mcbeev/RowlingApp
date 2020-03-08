using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RowlingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //Default implementation for Blazor server for CreateDefaultBuilder
            //Host.CreateDefaultBuilder(args)
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>();
            //    });

            //Adding in Azure App Configuration and Feature Flags to our Builder
            //ensure your app.
            Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                   var settings = config.Build();
                   config.AddAzureAppConfiguration(options => {
                       options.Connect(settings["ConnectionStrings:AppConfig"])
                              .UseFeatureFlags(); //with default options (caches every 30 seconds)
                   });


                })
                .UseStartup<Startup>());


            //  .UseFeatureFlags(opt => {
            //      opt.CacheExpirationTime = TimeSpan.FromSeconds(5); // Set cache expiry to 5 seconds
            //  });


        }
    }

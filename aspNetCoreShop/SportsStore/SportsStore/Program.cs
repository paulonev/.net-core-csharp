using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
//            BuildWebHost(args).Run();
            CreateHostBuilder(args).Build().Run();
        }

//        public static IWebHost BuildWebHost(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseStartup<Startup>()
//                .UseDefaultServiceProvider(options =>
//                    options.ValidateScopes = false)
//                .Build();
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { 
                    webBuilder
                    .UseStartup<Startup>()
                    .UseDefaultServiceProvider(options =>
                        options.ValidateScopes = false);
                });
    }
}
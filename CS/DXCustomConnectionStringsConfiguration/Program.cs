using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DXCustomConnectionStringsConfiguration {
    public class Program {
        public static Dictionary<string, string> connectionStrings = new Dictionary<string, string> {
                [$"ConnectionStrings:VehiclesInMemory"] = "XpoProvider=SQLite;Data Source=Data/vehicles.db"
        };

        public static ConfigurationProviderHelper configurationProviderHelper = new ConfigurationProviderHelper();

        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            var configuration = configurationProviderHelper.builder
            .AddInMemoryCollection(connectionStrings)
            .Build();
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseStartup<Startup>();
        }
    }
}
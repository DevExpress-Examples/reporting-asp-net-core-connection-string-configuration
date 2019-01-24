using DevExpress.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DXCustomConnectionStringsConfiguration
{
    public class ConfigurationProviderHelper
    {
        public void AssignConnectionStrings(IConfigurationRoot configuration) {
            var globalConnectionStrings = configuration
                .GetSection("ConnectionStrings")
                .AsEnumerable(true)
                .ToDictionary(x => x.Key, x => x.Value);
            DefaultConnectionStringProvider.AssignConnectionStrings(globalConnectionStrings);
        }
        public IConfigurationBuilder GetConfigurationBuilder(string contentRootPath, IHostingEnvironment hostingEnvironment) {
            return new ConfigurationBuilder()
                    .SetBasePath(contentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddXmlFile("customConfig.xml", optional: true, reloadOnChange: false)
                    .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        [$"ConnectionStrings:VehiclesInMemory"] = "XpoProvider=SQLite;Data Source=Data/vehicles.db"
                    })
                    .AddEnvironmentVariables();
        }
    }
}

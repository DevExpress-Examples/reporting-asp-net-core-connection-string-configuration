using DevExpress.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DXCustomConnectionStringsConfiguration {
    public class ConfigurationProviderHelper {
        public ConfigurationBuilder builder;
        public ConfigurationProviderHelper() {
            builder = new ConfigurationBuilder();
        }
        public void AssignConnectionStrings(IConfigurationRoot configuration) {
            var globalConnectionStrings = configuration
                .GetSection("ConnectionStrings")
                .AsEnumerable(true)
                .ToDictionary(x => x.Key, x => x.Value);
            DefaultConnectionStringProvider.AssignConnectionStrings(globalConnectionStrings);
        }
        public IConfigurationBuilder SetUpBuilder(string contentRootPath, IHostingEnvironment hostingEnvironment) {
            builder
                .SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);
            if(!hostingEnvironment.IsProduction()) {
                builder
                    .AddXmlFile("customConfig.xml", optional: true, reloadOnChange: false);
            }
            builder.AddEnvironmentVariables();
            return builder;
        }
    }
}

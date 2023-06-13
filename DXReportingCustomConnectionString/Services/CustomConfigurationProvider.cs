using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public class CustomConfigurationProvider {
    readonly IWebHostEnvironment hostingEnvironment;
    public CustomConfigurationProvider(IWebHostEnvironment hostingEnvironment) {
        this.hostingEnvironment = hostingEnvironment;
    }
    public IDictionary<string, string> GetGlobalConnectionStrings() {
        var connectionStrings = new Dictionary<string, string> {
            [$"ConnectionStrings:Vehicles_InMemory"] = "XpoProvider=SQLite;Data Source=Data/vehicles.db",
            [$"ConnectionStrings:Cars_InMemory"] = "XpoProvider=SQLite;Data Source=Data/cars.db;"
        };
        return new ConfigurationBuilder()
            .SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
            .AddInMemoryCollection(connectionStrings)
            .AddEnvironmentVariables()
            .Build()
            .GetSection("ConnectionStrings")
            .AsEnumerable(true)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    public IConfigurationSection GetReportDesignerWizardConfigurationSection() {
        return new ConfigurationBuilder()
            .SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
            .Build()
            .GetSection("ConnectionStrings");
    }
}

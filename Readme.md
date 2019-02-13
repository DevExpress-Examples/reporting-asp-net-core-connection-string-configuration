## Report Designer - How to read connection strings from different configuration sources in an ASP.NET Core application

In .NET Core applications, the default connection string provider implementation searches the **appsettings.json** file in the current directory and reads connection strings from the file’s **ConnectionStrings** section. 

This example demonstrates how to get connection strings from a set of different configuration sources in an ASP.NET Core application. 

Сreate a custom configuration and load connection strings from all the required sources. This example uses the [ConfigurationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder?view=aspnetcore-2.2) class and its [AddJsonFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.jsonconfigurationextensions.addjsonfile?view=aspnetcore-2.2) and [AddInMemoryCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.memoryconfigurationbuilderextensions.addinmemorycollection?view=aspnetcore-2.2) extension methods. See [Configuration in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/index?view=aspnetcore-2.2) for more information.

**Register Connection Strings Globally**

To register connection strings from a custom configuration globally, call the static **DefaultConnectionStringProvider.AssignConnectionStrings** method at the application startup.

These connection strings are available for the Report Designer's Preview to fill a report's data source and generate the resulting document.

In this example, global connection strings are read from the following sources:
* **appsettings.json** file;
* **appsettings.Development.json** file;
* in-memory collection.

**Register Connection Strings for the Report Designer's SQL Data Source Wizard**

To provide connection strings from a custom configuration to the Report Designer, call the [ReportDesignerConfigurationBuilder.RegisterDataSourceWizardConfigurationConnectionStringsProvider](https://docs.devexpress.com/XtraReports/DevExpress.AspNetCore.Reporting.ReportDesignerConfigurationBuilder.RegisterDataSourceWizardConfigurationConnectionStringsProvider(IConfigurationSection)) method at the application startup.

These connection strings are displayed in the [SQL Data Source Wizard](https://docs.devexpress.com/XtraReports/114093/create-end-user-reporting-applications/web-reporting/asp-net-webforms-reporting/end-user-report-designer/gui/wizards/sql-data-source-wizard) to create new data sources. 

Note that the SQL Data Source Wizard uses only the specified set of connection strings not including the strings registered globally.

In this example, the Report Designer's connection strings are read from the following files:
* **appsettings.json** 
* **appsettings.Development.json**

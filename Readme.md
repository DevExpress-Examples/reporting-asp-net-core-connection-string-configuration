<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/167363651/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830472)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Reporting for ASP.NET Core - Configuration-Dependent Connection Strings for Report Designer

## Implementation

In .NET Core applications, the default connection string provider implementation searches the `appsettings.json` file in the current directory and reads connection strings from the file’s `ConnectionStrings` section. 

This example demonstrates how to get connection strings from a set of different configuration sources in an ASP.NET Core application. 

This example implements a custom configuration and loads connection strings from various data sources. This example uses the [ConfigurationBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder) class and its [AddJsonFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.jsonconfigurationextensions.addjsonfile) and [AddInMemoryCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.memoryconfigurationbuilderextensions.addinmemorycollection) extension methods. 

### Register Connection Strings Globally

To register connection strings from a custom configuration globally, call the static [DefaultConnectionStringProvider.AssignConnectionStrings](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.DefaultConnectionStringProvider.AssignConnectionStrings.overloads) method at the application startup.

These connection strings are available for the Report Designer's Preview to fill a report's data source and generate the resulting document.

In this example, global connection strings are read from the following sources:

* `appsettings.json` file;
* `appsettings.Development.json` file;
* in-memory collection.

### Register Connection Strings For the Data Source Wizard

The Report Designer obtains connection strings from a custom configuration using the [RegisterDataSourceWizardConfigurationConnectionStringsProvider](https://docs.devexpress.com/XtraReports/DevExpress.AspNetCore.Reporting.ReportDesignerConfigurationBuilder.RegisterDataSourceWizardConfigurationConnectionStringsProvider(IConfigurationSection)) method at the application startup.

The obtained connection strings are shown in the [SQL Data Source Wizard](https://docs.devexpress.com/XtraReports/114093/create-end-user-reporting-applications/web-reporting/asp-net-webforms-reporting/end-user-report-designer/gui/wizards/sql-data-source-wizard) in the section that prompts the user to create new data sources. Note that the SQL Data Source Wizard uses only the specified set of connection strings, not including the strings registered globally.

In this example, the Report Designer's connection strings are read from the following files:

* `appsettings.json` 
* `appsettings.Development.json`

## Files to Review

- [Startup.cs](DXReportingCustomConnectionString/Startup.cs)
- [CustomConfigurationProvider.cs](DXReportingCustomConnectionString/Services/CustomConfigurationProvider.cs)
- [appsettings.json](DXReportingCustomConnectionString/appsettings.json)
- [appsettings.Development.json](DXReportingCustomConnectionString/appsettings.Development.json)
- [HomeController.cs](DXReportingCustomConnectionString/Controllers/HomeController.cs)
- [Designer.cshtml](DXReportingCustomConnectionString/Views/Home/Designer.cshtml)

## Documentation

- [Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration)
- [Change SQL Data Source Connection Settings at Runtime](https://docs.devexpress.com/XtraReports/401898/detailed-guide-to-devexpress-reporting/bind-reports-to-data/sql-database/change-sql-datasource-connection-settings-at-runtime)
- [Data Source Wizard - Choose a Data Connection](https://docs.devexpress.com/XtraReports/117578/web-reporting/gui/wizards/data-source-wizard-popup/add-a-new-data-source/choose-a-data-connection)
- [Web Report Designer - Register SQL Data Connections](https://docs.devexpress.com/XtraReports/400207/web-reporting/asp-net-mvc-reporting/end-user-report-designer-in-asp-net-mvc-applications/bind-to-data/register-sql-data-connections)
- [Data Sources in Web End-User Report Designer (ASP.NET Core)](https://docs.devexpress.com/XtraReports/401896/web-reporting/asp-net-core-reporting/end-user-report-designer-in-asp-net-applications/use-data-sources-and-connections)

## More Examples

- [ASP.NET Core Reporting - Best Practices](https://github.com/DevExpress-Examples/AspNetCore.Reporting.BestPractices)

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-connection-string-configuration&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-connection-string-configuration&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->

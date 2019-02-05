using System.Collections.Generic;
using DevExpress.DataAccess.Sql;
using DXCustomConnectionStringsConfiguration.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DXCustomConnectionStringsConfiguration.Controllers {
    public class HomeController : Controller {
        IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env) {
            _env = env;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Designer() {
            return View(new DesignerModel { ReportID = "Report", DataSources = setAvailableDataSources });
        }

        SqlDataSource createDataSource(string connectionStringName, SelectQuery query) {
            SqlDataSource ds = new SqlDataSource(connectionStringName);
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            return ds;
        }

        public void setAvailableDataSources(Dictionary<string, object> dataSources) {
            dataSources.Add("Northwind", createDataSource("NorthwindFromJson", SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products")));
            dataSources.Add("Countries", createDataSource($"Countries{_env.EnvironmentName}FromEnvJson", SelectQueryFluentBuilder.AddTable("Regions").SelectAllColumnsFromTable().Build("Regions")));
            dataSources.Add("Vehicles", createDataSource("VehiclesInMemory", SelectQueryFluentBuilder.AddTable("Model").SelectAllColumnsFromTable().Build("Model")));
            if(_env.EnvironmentName == "Development") {
                dataSources.Add("Cars", createDataSource("CarsFromXml", SelectQueryFluentBuilder.AddTable("Cars").SelectAllColumnsFromTable().Build("Cars")));
            }
        }
    }
}
using System.Collections.Generic;
using DevExpress.DataAccess.Sql;
using DXCustomConnectionStringsConfiguration.Models;
using Microsoft.AspNetCore.Mvc;

namespace DXCustomConnectionStringsConfiguration.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Designer() {
            return View(new DesignerModel { ReportID = "Report", DataSources = GetAvailableDataSources() });
        }

        public SqlDataSource CreateDataSource(string connectionStringName, SelectQuery query) {
            SqlDataSource ds = new SqlDataSource(connectionStringName);
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            return ds;
        }

        public Dictionary<string, object> GetAvailableDataSources() {
            var dataSources = new Dictionary<string, object>();
            dataSources.Add("Northwind", CreateDataSource("Northwind_Json", SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products")));
            dataSources.Add("Countries", CreateDataSource("Countries_DevelopmentJson", SelectQueryFluentBuilder.AddTable("Regions").SelectAllColumnsFromTable().Build("Regions")));
            dataSources.Add("Vehicles", CreateDataSource("Vehicles_InMemory", SelectQueryFluentBuilder.AddTable("Model").SelectAllColumnsFromTable().Build("Model")));
            dataSources.Add("Cars", CreateDataSource("Cars_InMemory", SelectQueryFluentBuilder.AddTable("Cars").SelectAllColumnsFromTable().Build("Cars")));
            return dataSources;
        }
    }
}
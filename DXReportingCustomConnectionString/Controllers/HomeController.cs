using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;


namespace DXReportingCustomConnectionString.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public async Task<IActionResult> Designer(
            [FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator,
            [FromQuery] string reportName) {
            Models.ReportDesignerCustomModel model = new Models.ReportDesignerCustomModel();
            model.ReportDesignerModel = await CreateDefaultReportDesignerModel(clientSideModelGenerator, reportName, null);
            return View(model);
        }

         public static Dictionary<string, object> GetAvailableDataSources() {
            var dataSources = new Dictionary<string, object> {
                { "Northwind", CreateDataSource("Northwind_Json", SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products")) },
                { "Countries", CreateDataSource("Countries_DevelopmentJson", SelectQueryFluentBuilder.AddTable("Regions").SelectAllColumnsFromTable().Build("Regions")) },
                { "Vehicles", CreateDataSource("Vehicles_InMemory", SelectQueryFluentBuilder.AddTable("Model").SelectAllColumnsFromTable().Build("Model")) },
                { "Cars", CreateDataSource("Cars_InMemory", SelectQueryFluentBuilder.AddTable("Cars").SelectAllColumnsFromTable().Build("Cars")) }
            };
            return dataSources;
        }

        public static SqlDataSource CreateDataSource(string connectionStringName, SelectQuery query) {
            SqlDataSource ds = new SqlDataSource(connectionStringName);
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            return ds;
        }

        public static async Task<ReportDesignerModel> CreateDefaultReportDesignerModel(IReportDesignerClientSideModelGenerator clientSideModelGenerator, string reportName, XtraReport report) {
            reportName = string.IsNullOrEmpty(reportName) ? "TestReport" : reportName;
            var dataSources = GetAvailableDataSources();
            if(report != null) {
                return await clientSideModelGenerator.GetModelAsync(report, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            }
            return await clientSideModelGenerator.GetModelAsync(reportName, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
        }
    }
}

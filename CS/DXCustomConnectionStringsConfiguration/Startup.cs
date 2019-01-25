using System.IO;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace DXCustomConnectionStringsConfiguration {
    public class Startup {
        string contentRootPath;
        IHostingEnvironment hostingEnvironment;
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;
            contentRootPath = hostingEnvironment.ContentRootPath;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IFileProvider FileProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDevExpressControls();
            var configurationProviderHelper = new ConfigurationProviderHelper(hostingEnvironment.IsProduction());
            var configurationBuilder = configurationProviderHelper.GetConfigurationBuilder(contentRootPath, hostingEnvironment);
            var reportCustomConfiguration = configurationBuilder.Build();
            // configurationProviderHelper.AssignConnectionStrings(reportCustomConfiguration);

            services.ConfigureReportingServices((builder) => {
                IConfigurationSection connectionStringsSection = reportCustomConfiguration.GetSection("ConnectionStrings");
                builder.ConfigureReportDesigner(designer => {
                    designer.RegisterDataSourceWizardConfigurationConnectionStringsProvider(connectionStringsSection);
                });
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            var reportDirectory = Path.Combine(env.ContentRootPath, "Reports");
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new ReportStorageWebExtension1(reportDirectory));
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Expressions;
            app.UseDevExpressControls();
            var configurationProviderHelper = new ConfigurationProviderHelper(hostingEnvironment.IsProduction());
            var configurationBuilder = configurationProviderHelper.GetConfigurationBuilder(contentRootPath, hostingEnvironment);
            var reportCustomConfiguration = configurationBuilder.Build();
            configurationProviderHelper.AssignConnectionStrings(reportCustomConfiguration);
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
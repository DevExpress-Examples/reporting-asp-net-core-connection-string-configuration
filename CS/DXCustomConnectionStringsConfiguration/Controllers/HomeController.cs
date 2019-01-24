using DXCustomConnectionStringsConfiguration.Models;
using Microsoft.AspNetCore.Mvc;

namespace DXCustomConnectionStringsConfiguration.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Designer() {
            var model = new DesignerModel {
                DataSources = null
            };
            return View(model);
        }
    }
}
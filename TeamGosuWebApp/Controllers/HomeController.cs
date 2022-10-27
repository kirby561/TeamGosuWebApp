using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamGosuWebApp.Models;
using TeamGosuWebApp.Services;

namespace TeamGosuWebApp.Controllers
{
    public class HomeController : Controller {
        private NewsManager _newsManager;

        public HomeController(NewsManager newsManager) {
            _newsManager = newsManager;
        }

        public IActionResult Index() {
            return View(new HomeModel(_newsManager));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

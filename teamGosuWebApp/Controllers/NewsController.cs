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
    public class NewsController : Controller {
        private NewsManager _newsManager;

        public NewsController(NewsManager newsManager) {
            _newsManager = newsManager;
        }

        public IActionResult Index(int page = 1) {
            return View(new NewsModel(_newsManager, page));
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

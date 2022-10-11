using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamGosuWebApp.Models;

namespace TeamGosuWebApp.Controllers {
    public class BeefController : Controller {
        public IActionResult Index() {
            
            return View(new BeefModel());
        }
    }
}

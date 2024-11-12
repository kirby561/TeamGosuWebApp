using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamGosuWebApp.Models;
using TeamGosuWebApp.Services;

namespace TeamGosuWebApp.Controllers
{
    public class BracketChallengeController : Controller {
        public BracketChallengeController() {
        }

        public IActionResult Pr10BracketChallenge() {
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

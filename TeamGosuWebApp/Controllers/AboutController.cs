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
    public class AboutController : Controller {
        private String _assemblyVersion;
        private String _assemblyName;

        public AboutController() {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            _assemblyName = assembly.GetName().Name;
            _assemblyVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        public IActionResult Index() {
            return View(new AboutModel(_assemblyVersion, _assemblyName));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

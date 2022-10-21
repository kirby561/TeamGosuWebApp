using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TeamGosuWebApp.Models;

namespace TeamGosuWebApp.Controllers {
    public class BeefController : Controller {
        // TODO: set this with a config file
        private const String BeefBotUrl = "http://localhost:5000/beef-ladder";

        public async Task<IActionResult> Index() {
            HttpClient client = new HttpClient();
            BeefModel model;
            try {
                HttpResponseMessage result = await client.GetAsync(BeefBotUrl);
                String contents = await result.Content.ReadAsStringAsync();
                model = new BeefModel(contents);
            } catch (HttpRequestException ex) {
                // Error
                model = new BeefModel();
            }

            return View(model);
        }
    }
}

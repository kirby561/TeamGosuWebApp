using Microsoft.AspNetCore.Mvc;
using TeamGosuWebApp.Models;
using TeamGosuWebApp.Services;

namespace TeamGosuWebApp.Controllers {
    public class TeamController  : Controller {
        
        private TeamsManager _teamsManager;

        public TeamController(TeamsManager teamsManager) {
            _teamsManager = teamsManager;
        }

        public IActionResult Index(int team = 0) {
            return View(new TeamPageModel(_teamsManager.GetTeams(), team));
        }
    }
}

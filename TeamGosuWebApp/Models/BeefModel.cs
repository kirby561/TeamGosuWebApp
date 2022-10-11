using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamGosuWebApp.Models {
    public class BeefEntry {
        public String Name { get; }
        public String Race { get; }
        public String Mmr { get; }
        public String RaceIconPath {
            get {
                return "/img/icons/" + Race + ".png";
            }
        }

        public BeefEntry(String name, String race, String mmr) {
            Name = name;
            Race = race;
            Mmr = mmr;
        }
    }

    public class BeefModel : PageModel {
        public BeefModel() {
            
        }

        public BeefEntry[] BeefLadderEntries = new BeefEntry[] { 
            new BeefEntry("Scrubbles", "Zerg", "5500"),
            new BeefEntry("Kirby", "Terran", "4300"),
            new BeefEntry("Lyra", "Terran", "5100"),
            new BeefEntry("Parmigiano", "Zerg", "4100"),
            new BeefEntry("Noahfecks", "Terran", "3800"),
            new BeefEntry("Cloud", "Zerg", "3800"),
            new BeefEntry("Fuzzy", "Protoss", "3100"),
            new BeefEntry("Thor", "Terran", "3800"),
            new BeefEntry("Guildelin", "Zerg", "4400"),
            new BeefEntry("Kinger", "Zerg", "3000"),
            new BeefEntry("Pastry", "Random", "5400"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Rolch", "Terran", "3300"),
            new BeefEntry("Brandon", "Zerg", "4300")};
    }
}
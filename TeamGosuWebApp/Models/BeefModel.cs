using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamGosuWebApp.Models {
    public class GetLadderResponse {
        public BeefEntry[] BeefLadder { get; set; }
    }

    /**
     * An entry in the beef ladder. This is the same form as the 
     */
    public class BeefEntry {
        public long Rank { get; set; }
        public String BeefName { get; set; }
        public String Race { get; set; }
        public String Mmr { get; set; }
        public String RaceIconPath {
            get {
                if (String.IsNullOrWhiteSpace(Race))
                    return "/img/icons/Unknown.png";
                return "/img/icons/" + Race + ".png";
            }
        }

        public String MmrUser {
            get {
                if (String.IsNullOrWhiteSpace(Mmr))
                    return "N/A";
                else
                    return Mmr;
            }
        }

        public BeefEntry() { }

        public BeefEntry(long rank, String name, String race, String mmr) {
            Rank = rank;
            BeefName = name;
            Race = race;
            Mmr = mmr;
        }
    }

    public class BeefModel : PageModel {
        public BeefModel() {
        }

        public BeefModel(String beefModelJson) {
            GetLadderResponse response = JsonConvert.DeserializeObject<GetLadderResponse>(beefModelJson);
            if (response != null)
                BeefLadderEntries = response.BeefLadder;
        }

        public BeefEntry[] BeefLadderEntries { get; set; } = new BeefEntry[] { 
            new BeefEntry(1, "Error", "", ""),
        };

        private BeefEntry[] _testEntries = new BeefEntry[] { 
            new BeefEntry(1, "Scrubbles", "Zerg", "5500"),
            new BeefEntry(2, "Kirby", "Terran", "4300"),
            new BeefEntry(3, "Lyra", "Terran", "5100"),
            new BeefEntry(4, "Parmigiano", "Zerg", "4100"),
            new BeefEntry(5, "Noahfecks", "Terran", "3800"),
            new BeefEntry(6, "Cloud", "Zerg", "3800"),
            new BeefEntry(7, "Fuzzy", "Protoss", "3100"),
            new BeefEntry(8, "Thor", "Terran", "3800"),
            new BeefEntry(9, "Guildelin", "Zerg", "4400"),
            new BeefEntry(10, "Kinger", "Zerg", "3000"),
            new BeefEntry(11, "Pastry", "Random", "5400"),
            new BeefEntry(12, "Rolch", "Terran", "3300"),
            new BeefEntry(13, "Rolch", "Terran", "3300"),
            new BeefEntry(14, "Rolch", "Terran", "3300"),
            new BeefEntry(15, "Rolch", "Terran", "3300"),
            new BeefEntry(16, "Rolch", "Terran", "3300"),
            new BeefEntry(17, "Rolch", "Terran", "3300"),
            new BeefEntry(18, "Rolch", "Terran", "3300"),
            new BeefEntry(19, "Rolch", "Terran", "3300"),
            new BeefEntry(20, "Rolch", "Terran", "3300"),
            new BeefEntry(21, "Rolch", "Terran", "3300"),
            new BeefEntry(22, "Rolch", "Terran", "3300"),
            new BeefEntry(23, "Rolch", "Terran", "3300"),
            new BeefEntry(24, "Rolch", "Terran", "3300"),
            new BeefEntry(25, "Rolch", "Terran", "3300"),
            new BeefEntry(26, "Rolch", "Terran", "3300"),
            new BeefEntry(27, "Rolch", "Terran", "3300"),
            new BeefEntry(28, "Rolch", "Terran", "3300"),
            new BeefEntry(29, "Rolch", "Terran", "3300"),
            new BeefEntry(30, "Rolch", "Terran", "3300"),
            new BeefEntry(31, "Rolch", "Terran", "3300"),
            new BeefEntry(32, "Rolch", "Terran", "3300"),
            new BeefEntry(33, "Rolch", "Terran", "3300"),
            new BeefEntry(34, "Rolch", "Terran", "3300"),
            new BeefEntry(35, "Rolch", "Terran", "3300"),
            new BeefEntry(36, "Rolch", "Terran", "3300"),
            new BeefEntry(37, "Rolch", "Terran", "3300"),
            new BeefEntry(38, "Rolch", "Terran", "3300"),
            new BeefEntry(39, "Rolch", "Terran", "3300"),
            new BeefEntry(40, "Rolch", "Terran", "3300"),
            new BeefEntry(41, "Rolch", "Terran", "3300"),
            new BeefEntry(42, "Rolch", "Terran", "3300"),
            new BeefEntry(43, "Rolch", "Terran", "3300"),
            new BeefEntry(44, "Rolch", "Terran", "3300"),
            new BeefEntry(45, "Brandon", "Zerg", "4300")};
    }
}
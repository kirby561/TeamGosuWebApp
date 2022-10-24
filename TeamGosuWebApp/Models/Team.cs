using System.Collections.Generic;

namespace TeamGosuWebApp.Models {
    public class Team {
        public int Priority { get; set; } = 0; // Determines the order in the list of teams
        public string Name { get; set; }
        public List<Player> Players { get; set; }
    }
}

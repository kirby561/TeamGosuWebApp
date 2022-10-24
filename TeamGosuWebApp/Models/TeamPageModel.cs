using System.Collections.Generic;

namespace TeamGosuWebApp.Models {
    public class TeamPageModel {
        private List<Team> _teams;
        private int _selectedTeamIndex;

        public TeamPageModel(List<Team> teams, int selectedTeamIndex) {
            _teams = teams;
            _selectedTeamIndex = selectedTeamIndex;
        }

        public List<Team> GetTeams() {
            return _teams;
        }

        public int GetSelectedTeamIndex() {
            return _selectedTeamIndex;
        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System;
using TeamGosuWebApp.Models;

namespace TeamGosuWebApp.Services {
    /// <summary>
    /// Manages loading the files in the teams directory and maintaining the player lists.
    /// </summary>
    public class TeamsManager {
        private String _teamsPath;

        private List<Team> _teams;

        private FileSystemWatcher _watcher;
        private Timer _antiSpamTimer = null;
        private object _antiSpamTimerLock = new object();
        
        public TeamsManager(string teamsPath) {
            _teamsPath = teamsPath;
            LoadTeams();
            MonitorTeamsDirectory();
        }

        public List<Team> GetTeams() {
            return _teams;
        }

        private void MonitorTeamsDirectory() {
            _watcher = new FileSystemWatcher();
            _watcher.Path = _teamsPath;
            _watcher.Filter = "*";
            _watcher.IncludeSubdirectories = true;
            _watcher.NotifyFilter = 
                NotifyFilters.LastAccess | 
                NotifyFilters.LastWrite | 
                NotifyFilters.FileName | 
                NotifyFilters.DirectoryName;

            _watcher.Changed += new FileSystemEventHandler(OnFileOrDirectoryChanged);
            _watcher.Created += new FileSystemEventHandler(OnFileOrDirectoryChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnFileOrDirectoryChanged);
            _watcher.Renamed += new RenamedEventHandler(OnFileOrDirectoryRenamed);

            _watcher.EnableRaisingEvents = true;
        }

        private void OnFileOrDirectoryChanged(object sender, FileSystemEventArgs args) {
            Console.WriteLine("Teams directory changed. Thread = " + Thread.CurrentThread.ManagedThreadId);
            // Start a timer to update news articles in 1 minute.
            // Reset this timer each time this is called so we aren't spamming the loading process.
            lock (_antiSpamTimerLock) {
                if (_antiSpamTimer != null) {
                    _antiSpamTimer.Dispose();
                    _antiSpamTimer = null;
                }
                _antiSpamTimer = new Timer(OnAntiSpamNewsTimerExpired, null, 60000, Timeout.Infinite);
            }
        }

        private void OnFileOrDirectoryRenamed(object sender, RenamedEventArgs args) {
            OnFileOrDirectoryChanged(sender, null);
        }

        private void OnAntiSpamNewsTimerExpired(object state) {
            Console.WriteLine("Timer expired. Thread = " + Thread.CurrentThread.ManagedThreadId);
            lock (_antiSpamTimerLock) {
                _antiSpamTimer.Dispose();
                _antiSpamTimer = null;
            }

            LoadTeams();
        }

        private void LoadTeams() {
            Console.WriteLine("Loading teams...");
            DateTime beforeTime = DateTime.Now;

            List<Team> teams = new List<Team>();
            foreach (String file in Directory.EnumerateFiles(_teamsPath)) {
                try {
                    String fileText = File.ReadAllText(file);

                    Team team = JsonConvert.DeserializeObject<Team>(fileText);

                    teams.Add(team);
                } catch (Exception e) {
                    Console.WriteLine("Error reading team file " + file + ".\n" + e.Message);
                    continue;
                }
            }

            // Sort by priority (lower priority number means put that one first)
            teams.Sort(new TeamSorter());

            // This operation is atomic in C#.
            // However, care needs to be taken that if the size of the
            // teams list changes, other threads using it are prepared
            // for the list to change suddenly.
            _teams = teams;

            DateTime afterTime = DateTime.Now;
            TimeSpan timeSpan = afterTime - beforeTime;
            Console.WriteLine("...Finished loading teams. It took " + timeSpan.TotalMilliseconds + "ms");
        }

        private class TeamSorter : IComparer<Team> {
            public int Compare(Team x, Team y) {
                if (x.Priority < y.Priority)
                    return -1;
                else if (x.Priority > y.Priority)
                    return 1;
                else
                    return x.Name.CompareTo(y.Name);
            }
        }
    }
}

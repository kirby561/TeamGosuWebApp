using Microsoft.Extensions.FileSystemGlobbing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TeamGosuWebApp.Models;

namespace TeamGosuWebApp.Services {
    /// <summary>
    /// Reads the news and provides it to controllers asking for it.
    /// 
    /// News is stored in the /news directory. Each story should be a directory in the following form:
    ///     yyyy-mm-dd-[optional-name]
    ///         info.json
    ///         image.png
    ///         content.html
    /// </summary>
    public class NewsManager {
        private const String InfoFileName = "info.json";
        private const String ContentFileName = "content.html";
        private const String ImageFileName = "image.png";

        private String _newsPath;
        private String _wwwRoot;

        private List<NewsStory> _newsStories;

        private FileSystemWatcher _watcher;
        private Timer _antiSpamTimer = null;
        private object _antiSpamTimerLock = new object();
        
        public NewsManager(string newsPath, string wwwRoot) {
            _newsPath = newsPath;
            _wwwRoot = wwwRoot;
            LoadNews();
            MonitorNewsDirectory();
        }

        public int GetNumNewsStories() {
            return _newsStories.Count;
        }

        public NewsStory GetStoryAt(int index) {
            return _newsStories[index];
        }

        private void MonitorNewsDirectory() {
            _watcher = new FileSystemWatcher();
            _watcher.Path = _newsPath;
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
            Console.WriteLine("News directory changed. Thread = " + Thread.CurrentThread.ManagedThreadId);
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

            LoadNews();
        }

        private void LoadNews() {
            Console.WriteLine("Loading news stories...");
            DateTime beforeTime = DateTime.Now;

            int wwwRootIndex = _newsPath.IndexOf(_wwwRoot);;
            if (wwwRootIndex != 0)
                throw new ArgumentException("The news path needs to be in wwwroot somewhere.");
            String wwwPrefix = _newsPath.Substring(_wwwRoot.Length + 1);

            List<NewsStory> newsStories = new List<NewsStory>();
            foreach (String directory in Directory.EnumerateDirectories(_newsPath)) {
                String subPath = directory.Substring(_newsPath.Length + 1);

                // Make sure there is an image, a content.html and an info.json
                if (!File.Exists(directory + "/" + InfoFileName)) {
                    Console.WriteLine("No " + InfoFileName + " file associated with news story at " + directory);
                    continue;
                }
                if (!File.Exists(directory + "/" + ContentFileName)) {
                    Console.WriteLine("No " + ContentFileName + " file associated with news story at " + directory);
                    continue;
                }
                if (!File.Exists(directory + "/" + ImageFileName)) {
                    Console.WriteLine("No " + ImageFileName + " file associated with news story at " + directory);
                    continue;
                }

                try {
                    String infoFileText = File.ReadAllText(directory + "/" + InfoFileName);
                    NewsStoryInfoFile info = JsonConvert.DeserializeObject<NewsStoryInfoFile>(infoFileText);
                    String content = File.ReadAllText(directory + "/" + ContentFileName);
                    String imageUrl = wwwPrefix + "/" + subPath + "/" + ImageFileName;

                    NewsStory story = new NewsStory() {
                        PublishDate = DateTime.Parse(info.PublishDate),
                        ImageUrl = imageUrl,
                        Title = info.Title,
                        Content = content,
                        DetailsUrl = info.DetailsUrl
                    };

                    newsStories.Add(story);
                } catch (Exception e) {
                    Console.WriteLine("Error reading news story " + directory + ".\n" + e.Message);
                    continue;
                }
            }

            // Sort the stories
            newsStories.Sort(new NewsStorySorter());
            
            // This operation is atomic in C#.
            // However, care needs to be taken that if the size of the
            // news stories list changes, other threads using it are prepared
            // for the list to change suddenly.
            _newsStories = newsStories;

            DateTime afterTime = DateTime.Now;
            TimeSpan timeSpan = afterTime - beforeTime;
            Console.WriteLine("...Finished loading news stories. It took " + timeSpan.TotalMilliseconds + "ms");
        }
    }

    /// <summary>
    /// Represents the info.json file contents. The info.json file should have the following contents:
    ///         {
    ///         	"Title": "Some Title",
    ///         	"PublishDate": "yyyy-mm-ddThh:mm:ss",
    ///         	"DetailsUrl": "[external link to more info]"
    ///         }
    /// The DetailsUrl is optional and if it's null that link will not be shown.
    /// The PublishDate is in UTC
    /// </summary>
    public class NewsStoryInfoFile {
        public String PublishDate { get; set; }
        public String Title { get; set; }
        public String DetailsUrl { get; set; }
    }

    public class NewsStorySorter : IComparer<NewsStory> {
        public int Compare(NewsStory x, NewsStory y) {
            return -1 * x.PublishDate.CompareTo(y.PublishDate);
        }
    }
}

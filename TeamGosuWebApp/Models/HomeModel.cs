using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using TeamGosuWebApp.Services;
using TeamGosuWebApp.Utility;

namespace TeamGosuWebApp.Models {
    public class HomeModel : PageModel {
        private const int NumNewsStoriesToShowOnHomePage = 3;

        private NewsManager _newsManager;

        private List<NewsStory> _latestStories = new List<NewsStory>();

        public HomeModel(NewsManager newsManager) {
            _newsManager = newsManager;

            int numStoriesToShow = Math.Min(NumNewsStoriesToShowOnHomePage, _newsManager.GetNumNewsStories());
            for (int i = 0; i < numStoriesToShow; i++)
                _latestStories.Add(_newsManager.GetStoryAt(i));
        }

        public List<NewsStory> GetLatestStories() {
            return _latestStories;
        }

        public String GetUiPublishDate(NewsStory story) {
            return DateDisplayHelper.GetDisplayedDateString(story.PublishDate);
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using TeamGosuWebApp.Services;
using TeamGosuWebApp.Utility;

namespace TeamGosuWebApp.Models {
    public class NewsModel : PageModel {
        private const int StoriesPerPage = 3;
        private NewsManager _newsManager;
        private int _page;

        public NewsModel(NewsManager newsManager, int pageOneBased) {
            _newsManager = newsManager;
            _page = pageOneBased;

            if (_page < 1) _page = 1;
            if (_page > GetNumPages()) _page = GetNumPages();
        }

        public int GetNumPages() {
            return (int)Math.Ceiling(Convert.ToDecimal(_newsManager.GetNumNewsStories()) / Convert.ToDecimal(StoriesPerPage));
        }

        public int GetCurrentPage() {
            return _page;
        }

        public List<NewsStory> GetStoriesOnPage() {
            // Stories are shown in chronological order
            List<NewsStory> pageStories = new List<NewsStory>();
            int pageStartIndex = StoriesPerPage * (_page - 1);
            for (int i = pageStartIndex; i < Math.Min(pageStartIndex + StoriesPerPage, _newsManager.GetNumNewsStories()); i++)
                pageStories.Add(_newsManager.GetStoryAt(i));
            return pageStories;
        }
        
        public String GetUiPublishDate(NewsStory story) {
            return DateDisplayHelper.GetDisplayedDateString(story.PublishDate);
        }
    }
}

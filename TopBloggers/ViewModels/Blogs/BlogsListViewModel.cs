using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Blogs
{
    public class BlogsListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public string Search { get; set; }
        public int TotalArticles { get; set; }
    }
}
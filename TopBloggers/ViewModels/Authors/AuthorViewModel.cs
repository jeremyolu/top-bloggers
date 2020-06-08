using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Authors
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public List<Article> AuthorArticles { get; set; }
        public int TotalViews { get; set; }
        public string Url { get; set; }
    }
}
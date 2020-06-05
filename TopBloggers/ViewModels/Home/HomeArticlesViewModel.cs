using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Home
{
    public class HomeArticlesViewModel
    {
        public IEnumerable <Article> TopArticles { get; set; }
        public IEnumerable <Article> LatestArticles { get; set; }
        public IEnumerable <Article> FeaturedArticles { get; set; }
        public IEnumerable <Author> PopularAuthors { get; set; }
    }
}
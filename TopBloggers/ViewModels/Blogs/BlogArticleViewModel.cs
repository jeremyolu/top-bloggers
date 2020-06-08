using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Blogs
{
    public class BlogArticleViewModel
    {
        public Article BlogArticle { get; set; }
        public int BlogId { get; set; }
        public string FormattedTitle { get; set; }
        public string BlogArticleUrl { get; set; }
        public string AuthorUrl { get; set; }
        public List<Article> AuthorArticles { get; set; }
        public IEnumerable<Article> RelatedArticles { get; set; }
    }
}
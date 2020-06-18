using System.Collections.Generic;
using System.Text.RegularExpressions;
using TopBloggers.Models;

namespace TopBloggers.Settings.Helpers
{
    public static class Extensions
    {
        public static List<Article> GenerateUrls(this List<Article> articles)
        {
            foreach (var article in articles)
            {
                article.Url = $"{article.BlogArticleID}={GenerateFriendlyUrl(article.Title)}";
            }

            return articles;
        }

        public static List<Author> GenerateUrls(this List<Author> authors)
        {
            foreach (var author in authors)
            {
                author.AuthorUrl = $"{author.AuthorID}-{author.Name}{author.Surname}".ToLower();
            }

            return authors;
        }

        public static List<Article> ShortenDescription(this List<Article> articles)
        {
            foreach (var article in articles)
            {
                // shorten description to prompt read more...
                // article.Description = 
            }

            return articles;
        }

        private static string GenerateFriendlyUrl(string title)
        {
            var url = title.Replace(" ", "-").ToLower();

            return Regex.Replace(url, @"[^\w-]|_", string.Empty);
        }
    }
}
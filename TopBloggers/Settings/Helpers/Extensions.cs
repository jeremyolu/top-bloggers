using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TopBloggers.Models;

namespace TopBloggers.Settings.Helpers
{
    public static class Extensions
    {
        public static List<Article> GenerateArticleUrls(this List<Article> articles)
        {
            foreach (var article in articles)
            {
                article.Description = ShortenArticleDescription(article.Description, 25);
                article.Url = $"{article.BlogArticleID}={GenerateFriendlyUrl(article.Title)}";
                article.AuthorUrl = $"{article.Author.AuthorID}-{article.Author.Name}{article.Author.Surname}".ToLower();
            }

            return articles;
        }

        public static List<Author> GenerateAuthorUrls(this List<Author> authors)
        {
            foreach (var author in authors)
            {
                author.AuthorUrl = $"{author.AuthorID}-{author.Name}{author.Surname}".ToLower();
            }

            return authors;
        }

        public static string ShortenArticleDescription(this String description, int wordCount)
        {
            if (wordCount < 0)
            {
                throw new ArgumentOutOfRangeException("wordCount should be greater than or equal to 0");
            }

            if (wordCount == 0)
            {
                return string.Empty;
            }

            var words = description.Split(' ');

            if (words.Length <= wordCount)
            {
                return description;
            }

            return string.Join(" ", words.Take(wordCount)) + "...";
        }

        private static string GenerateFriendlyUrl(string title)
        {
            var url = title.Replace(" ", "-").ToLower();

            return Regex.Replace(url, @"[^\w-]|_", string.Empty);
        }
    }
}
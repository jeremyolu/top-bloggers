using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Models;
using TopBloggers.ViewModels.Blogs;
using TopBloggers.ViewModels.Home;

namespace TopBloggers.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IAuthorRepository _authorRepository;

        private const int MinTopLikesMark = 10;
        private const int FeaturedCalculateScoreMark = 50;
        //implement later
        private const int MinPopularAuthorArticles = 0;

        public BlogService(IBlogRepository blogRepository, IAuthorRepository authorRepository)
        {
            _blogRepository = blogRepository;
            _authorRepository = authorRepository;
        }

        public HomeArticlesViewModel GetHomeArticlesViewModel()
        {
            var topBlogArticles = _blogRepository.GetArticles().Where(l => l.Likes >= MinTopLikesMark).ToList();
            var latestBlogArticles = _blogRepository.GetLatestArticlesForCurrentMonth();
            var featuredBlogs = _blogRepository.GetArticles();
            var popularAuthors = _authorRepository.GetAuthors();

            var model = new HomeArticlesViewModel
            {
                TopArticles = topBlogArticles.GenerateUrls(),
                LatestArticles = latestBlogArticles.GenerateUrls(),
                FeaturedArticles = DetermineFeaturedArticle(featuredBlogs.GenerateUrls()),
                PopularAuthors = DeterminePopularAuthors(popularAuthors).GenerateUrls()
            };

            return model;
        }

        public BlogsListViewModel GetBlogArticles(string search = null)
        {
            var articles = _blogRepository.GetArticles(search);

            var model = new BlogsListViewModel
            {
                Articles = articles.GenerateUrls(),
                Search = search,
                TotalArticles = articles.Count
            };

            return model;
        }

        public BlogArticleViewModel GetBlogArticleViewModel(int id)
        {
            var blogArticle = _blogRepository.GetArticleById(id);

            if (blogArticle == null)
            {
                return new BlogArticleViewModel();
            }

            var formatTitle = GenerateFriendlyUrl(blogArticle.Title);

            var authorArticles = _blogRepository.GetArticlesByAuthorId(blogArticle.Author.AuthorID).Where(a => a.BlogArticleID != id).ToList();
            var relatedArticles = _blogRepository.GetArticlesByCategoryId(blogArticle.CategoryID).Where(a => a.BlogArticleID != id).ToList();

            var author = _authorRepository.GetAuthorById(blogArticle.AuthorID);
            var authorUrl = $"{author.AuthorID}-{author.Name}{author.Surname}".ToLower();

            blogArticle.Title = CapitalizeTitle(blogArticle.Title);

            var formattedUrl = $"{id}={formatTitle}";

            var model = new BlogArticleViewModel
            {
                BlogArticle = blogArticle,
                BlogId = id,
                AuthorUrl = authorUrl,
                FormattedTitle = formatTitle,
                BlogArticleUrl = formattedUrl,
                AuthorArticles = authorArticles.GenerateUrls(),
                RelatedArticles = relatedArticles.GenerateUrls()
            };

            return model;
        }

        private string CapitalizeTitle(string title)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(title.ToLower()); 
        }

        private string GenerateFriendlyUrl(string title)
        {
            var url = title.Replace(" ", "-").ToLower();

            return Regex.Replace(url, @"[^\w-]|_", String.Empty);
        }

        private List<Article> DetermineFeaturedArticle(List<Article> articles)
        {
            var featured = new List<Article>();

            foreach (var blog in articles.ToList())
            {
                if (blog.Likes + blog.Views >= FeaturedCalculateScoreMark)
                {
                    featured.Add(blog);
                }
            }

            return featured;
        }

        private List<Author> DeterminePopularAuthors(List<Author> authors)
        {
            var popular = new List<Author>();

            foreach (var author in authors.ToList())
            {
                if (author.Articles.Count >= MinPopularAuthorArticles)
                {
                    popular.Add(author);
                }
            }

            return popular;
        }
    }

    public static class LinqExtension
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
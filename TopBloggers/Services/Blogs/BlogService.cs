using System.Collections.Generic;
using System.Linq;
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
            var topBlogArticles = _blogRepository.GetArticles().Where(l => l.Likes >= MinTopLikesMark);
            var latestBlogArticles = _blogRepository.GetLatestArticlesForCurrentMonth();
            var featuredBlogs = _blogRepository.GetArticles();
            var popularAuthors = _authorRepository.GetAuthors();

            var model = new HomeArticlesViewModel
            {
                TopArticles = topBlogArticles,
                LatestArticles = latestBlogArticles,
                FeaturedArticles = DetermineFeaturedArticle(featuredBlogs),
                PopularAuthors = DeterminePopularAuthors(popularAuthors)
            };

            return model;
        }

        public BlogsListViewModel GetBlogArticles(string search = null)
        {
            var articles = _blogRepository.GetArticles(search);

            var model = new BlogsListViewModel
            {
                Articles = articles,
                Search = search,
                TotalArticles = articles.Count
            };

            return model;
        }

        public BlogArticleViewModel GetBlogArticleViewModel(int id)
        {
            var blogArticle = _blogRepository.GetArticleById(id);
            var authorArticles = _blogRepository.GetArticlesByAuthorId(blogArticle.Author.AuthorID);
            var relatedArticles = _blogRepository.GetArticlesByCategoryId(blogArticle.CategoryID).Where(a => a.BlogArticleID != id);

            blogArticle.Title = CapitalizeTitle(blogArticle.Title);

            var model = new BlogArticleViewModel
            {
                BlogArticle = blogArticle,
                AuthorArticles = authorArticles,
                RelatedArticles = relatedArticles
            };

            return model;
        }

        private string CapitalizeTitle(string title)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }

        private string CreateFriendlyUrl(Article article)
        {
            var articleId = article.BlogArticleID;
            var articleTitle = article.Title;

            var url = $"{articleId} {articleTitle}";
            url = url.Replace(" ", "-");

            return url;
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
}
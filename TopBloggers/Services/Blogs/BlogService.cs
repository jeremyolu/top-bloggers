using System.Collections.Generic;
using System.Linq;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Models;
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

        private List <Article> DetermineFeaturedArticle(List<Article> articles)
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
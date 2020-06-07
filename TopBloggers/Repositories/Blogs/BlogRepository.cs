using System;
using System.Collections.Generic;
using System.Linq;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Models;

namespace TopBloggers.Repositories.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly TopBloggersDB _topBloggersDb;

        public BlogRepository()
        {
            _topBloggersDb = new TopBloggersDB();
        }

        public List <Article> GetArticles()
        {
            return _topBloggersDb.Articles.ToList();
        }

        public List<Article> GetArticles(string search)
        {
            return _topBloggersDb.Articles.OrderBy(a => a.Title).Where(a => a.Title.Contains(search) || a.Author.Name.Contains(search) || a.Author.Surname.Contains(search) || a.Category.Name.Contains(search) || search == null).ToList();
        }

        public Article GetArticleById(int id)
        {
            return _topBloggersDb.Articles.SingleOrDefault(a => a.BlogArticleID == id);
        }

        public List<Article> GetLatestArticlesForCurrentMonth()
        {
            return _topBloggersDb.Articles.Where(d => d.CreatedDate.Month == DateTime.Today.Month).ToList();
        }

        public List <Article> GetArticlesByAuthorId(int id)
        {
            return _topBloggersDb.Articles.Where(a => a.AuthorID == id).ToList();
        }

        public List<Article> GetArticlesByCategoryId(int id)
        {
            return _topBloggersDb.Articles.Where(ac => ac.CategoryID == id).ToList();
        }
    }
}
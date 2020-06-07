using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        List<Article> GetArticles();
        List<Article> GetArticles(string search);
        Article GetArticleById(int id);
        List<Article> GetLatestArticlesForCurrentMonth();
        List<Article> GetArticlesByAuthorId(int id);
        List<Article> GetArticlesByCategoryId(int id);
    }
}

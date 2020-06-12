using System.Web;
using TopBloggers.Models;
using TopBloggers.ViewModels.Account;

namespace TopBloggers.Interfaces.Services
{
    public interface IAccountService
    {
        Author Register(Author author, HttpPostedFileWrapper file);
        NewBlogArticleViewModel GetCategoryList();
        Article CreateNewBlogArticle(Article article, HttpPostedFileWrapper file);
        Author Login(Author author);
        AuthorAccountViewModel GetAuthorDashboard(string email);
    }
}
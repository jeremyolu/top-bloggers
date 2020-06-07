using TopBloggers.ViewModels.Blogs;
using TopBloggers.ViewModels.Home;

namespace TopBloggers.Interfaces.Services
{
    public interface IBlogService
    {
        HomeArticlesViewModel GetHomeArticlesViewModel();
        BlogsListViewModel GetBlogArticles(string search = null);
        BlogArticleViewModel GetBlogArticleViewModel(int id);
    }
}

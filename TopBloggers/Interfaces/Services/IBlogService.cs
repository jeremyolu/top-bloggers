using TopBloggers.ViewModels.Home;

namespace TopBloggers.Interfaces.Services
{
    public interface IBlogService
    {
        HomeArticlesViewModel GetHomeArticlesViewModel();
    }
}

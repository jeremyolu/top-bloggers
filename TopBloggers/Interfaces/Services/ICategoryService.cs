using TopBloggers.ViewModels.Category;

namespace TopBloggers.Interfaces.Services
{
    public interface ICategoryService
    {
        CategoryListViewModel GetCategoryListViewModel(string search = null);
        CategoryViewModel GetCategoryViewModel(int id);
    }
}

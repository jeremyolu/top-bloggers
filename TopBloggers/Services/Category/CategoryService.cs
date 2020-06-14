using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.ViewModels.Category;

namespace TopBloggers.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryListViewModel GetCategoryListViewModel(string search = null)
        {
            var categories = _categoryRepository.GetCategories(search);

            var model = new CategoryListViewModel
            {
                Categories = categories,
                Search = search,
                TotalCategories = categories.Count
            };

            return model;
        }
    }
}
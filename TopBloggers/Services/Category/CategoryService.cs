using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Services.Blogs;
using TopBloggers.Settings.Helpers;
using TopBloggers.ViewModels.Category;

namespace TopBloggers.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogRepository _blogRepository;

        public CategoryService(ICategoryRepository categoryRepository, IBlogRepository blogRepository)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
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

        public CategoryViewModel GetCategoryViewModel(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            var categoryArticles = _blogRepository.GetArticlesByCategoryId(id);

            var model = new CategoryViewModel
            {
                Category = category,
                Articles = categoryArticles.GenerateArticleUrls()
            };

            return model;
        }
    }
}
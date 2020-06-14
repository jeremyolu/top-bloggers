using System.Collections.Generic;
using System.Linq;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Models;

namespace TopBloggers.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TopBloggersDB _topBloggersDb;

        public CategoryRepository()
        {
            _topBloggersDb = new TopBloggersDB();
        }

        public List<Category> GetCategories(string search)
        {
            return _topBloggersDb.Categories.OrderBy(c => c.Name).Where(c => c.Name.Contains(search) || search == null).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _topBloggersDb.Categories.SingleOrDefault(c => c.CategoryID == id);
        }
    }
}
using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories(string search);
        Category GetCategoryById(int id);
    }
}

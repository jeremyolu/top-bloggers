using System.Collections.Generic;

namespace TopBloggers.ViewModels.Category
{
    public class CategoryListViewModel
    {
        public List<Models.Category> Categories { get; set; }
        public string Search { get; set; }
        public int TotalCategories { get; set; }
    }
}
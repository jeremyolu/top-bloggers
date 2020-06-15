using System.Collections.Generic;

namespace TopBloggers.ViewModels.Category
{
    public class CategoryViewModel
    {
        public Models.Category Category { get; set; }
        public List<Models.Article> Articles { get; set; }
    }
}
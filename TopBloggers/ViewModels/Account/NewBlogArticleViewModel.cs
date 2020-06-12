using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Account
{
    public class NewBlogArticleViewModel : AuthorAccountViewModel
    {
        public Article Article { get; set; }
        public List<Category> Categories { get; set; }
    }
}
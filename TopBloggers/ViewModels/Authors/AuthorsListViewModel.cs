using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.ViewModels.Authors
{
    public class AuthorsListViewModel
    {
        public List<Author> Authors { get; set; }
        public string Search { get; set; }
        public int TotalAuthors { get; set; }
    }
}
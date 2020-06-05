using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopBloggers.Models;

namespace TopBloggers.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthors();
    }
}

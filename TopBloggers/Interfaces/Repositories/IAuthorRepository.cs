using System.Collections.Generic;
using TopBloggers.Models;

namespace TopBloggers.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthors();
        List<Author> GetAuthors(string search);
        Author GetAuthorById(int id);
        Author GetAuthorByEmail(string email);
    }
}

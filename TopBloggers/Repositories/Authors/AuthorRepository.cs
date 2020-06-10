using System.Collections.Generic;
using System.Linq;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Models;

namespace TopBloggers.Repositories.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly TopBloggersDB _topBloggersDb;

        public AuthorRepository()
        {
            _topBloggersDb = new TopBloggersDB();
        }

        public List<Author> GetAuthors()
        {
            return _topBloggersDb.Authors.ToList();
        }

        public List<Author> GetAuthors(string search)
        {
            return _topBloggersDb.Authors.OrderBy(a => a.Name)
                .Where(a => a.Name.Contains(search) || a.Surname.Contains(search) || search == null).ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _topBloggersDb.Authors.SingleOrDefault(a => a.AuthorID == id);
        }

        public Author GetAuthorByEmail(string email)
        {
            return _topBloggersDb.Authors.FirstOrDefault(a => a.Email == email);
        }
    }
}
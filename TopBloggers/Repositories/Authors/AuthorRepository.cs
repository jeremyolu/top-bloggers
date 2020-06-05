using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
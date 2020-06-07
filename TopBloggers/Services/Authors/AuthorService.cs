using System.Linq;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Services.Blogs;
using TopBloggers.ViewModels.Authors;

namespace TopBloggers.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public AuthorsListViewModel GetAuthorsListViewModel(string search = null)
        {
            var authors = _authorRepository.GetAuthors(search).OrderBy(a => a.Surname).ToList();

            var model = new AuthorsListViewModel
            {
                Authors = authors.GenerateUrls(),
                Search = search,
                TotalAuthors = authors.Count
            };

            return model;
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return new AuthorViewModel();
            }

            var authorFormattedUrl = $"{author.Name}{author.Surname}".ToLower();

            var model = new AuthorViewModel()
            {
                Author = author,
                Url = authorFormattedUrl
            };

            return model;
        }
    }
}
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
        private readonly IBlogRepository _blogRepository;

        public AuthorService(IAuthorRepository authorRepository, IBlogRepository blogRepository)
        {
            _authorRepository = authorRepository;
            _blogRepository = blogRepository;
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
            var authorArticles = _blogRepository.GetArticlesByAuthorId(id);
            var totalViews = authorArticles.Sum(a => a.Views);

            if (author == null)
            {
                return new AuthorViewModel();
            }

            var authorFormattedName = $"{author.Name}{author.Surname}".ToLower();

            var model = new AuthorViewModel()
            {
                Author = author,
                TotalViews = totalViews,
                AuthorArticles = authorArticles.GenerateUrls(),
                Url = authorFormattedName
            };

            return model;
        }
    }
}
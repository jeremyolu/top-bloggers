using TopBloggers.ViewModels.Authors;

namespace TopBloggers.Interfaces.Services
{
    public interface IAuthorService
    {
        AuthorsListViewModel GetAuthorsListViewModel(string search = null);
        AuthorViewModel GetAuthorViewModel(int name);
    }
}

using System.Linq;
using System.Web.Mvc;
using TopBloggers.Interfaces.Services;

namespace TopBloggers.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public ActionResult Index(string search = null)
        {
            var model = _authorService.GetAuthorsListViewModel(search);

            return View(model);
        }

        [Route("authors/profile/{id}-{name}")]
        public new ActionResult Profile(int id, string name)
        {
            var model = _authorService.GetAuthorViewModel(id);

            if (id != model.Author?.AuthorID || name != model.Url)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}
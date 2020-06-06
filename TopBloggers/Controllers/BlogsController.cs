using System.Web.Mvc;
using TopBloggers.Interfaces.Services;

namespace TopBloggers.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index(string search = null)
        {
            var model = _blogService.GetBlogArticles(search);

            return View(model);
        }
    }
}
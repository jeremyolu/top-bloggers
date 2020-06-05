using System.Web.Mvc;
using TopBloggers.Interfaces.Services;

namespace TopBloggers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            var model = _blogService.GetHomeArticlesViewModel();

            return View(model);
        }
    }
}
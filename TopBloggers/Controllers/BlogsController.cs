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

        [Route("blogs/article/{id}={title}")]
        public ActionResult Article(int id, string title)
        {
            var model = _blogService.GetBlogArticleViewModel(id);

            if (id != model.BlogId || title != model.FormattedTitle)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}
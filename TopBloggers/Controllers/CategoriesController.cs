using System.Web.Mvc;
using TopBloggers.Interfaces.Services;

namespace TopBloggers.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index(string search = null)
        {
            var model = _categoryService.GetCategoryListViewModel(search);

            return View(model);
        }

        public ActionResult Cat()
        {
            return View();
        }
    }
}
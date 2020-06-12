using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TopBloggers.Interfaces.Services;
using TopBloggers.Models;
using TopBloggers.ViewModels.Account;

namespace TopBloggers.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly TopBloggersDB _topBloggersDb;

        public AccountController(IAccountService accountService)
        {
            _topBloggersDb = new TopBloggersDB();
            _accountService = accountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Author author)
        {
            var user = _accountService.Login(author);

            if (user != null)
            {
                Session["AUTHOR"] = author.Email;

                return RedirectToAction("Dashboard", author.AuthorID);
            }

            ViewBag.Message = "Incorrect credentials";

            return View(author);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name,Surname,Email,Password")] Author author, HttpPostedFileWrapper file)
        {
            if (ModelState.IsValid)
            {
                _accountService.Register(author, file);

                return RedirectToAction("Login");
            }

            return View(author);
        }

        public ActionResult Dashboard()
        {
            var email = (string) Session["AUTHOR"];

            if (email == null)
            {
                return RedirectToAction("Login");
            }

            var model = _accountService.GetAuthorDashboard(email);

            return View(model);
        }

        public ActionResult Create()
        {
            var email = (string)Session["AUTHOR"];
            var author = _accountService.GetAuthorDashboard(email).Author;
            var list = _accountService.GetCategoryList().Categories;

            if (email == null)
            {
                return RedirectToAction("Login");
            }

            var model = new NewBlogArticleViewModel
            {
                Author = author,
                Categories = list
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Text,AuthorID,CategoryID")] Article article, HttpPostedFileWrapper file)
        {
            var email = (string)Session["AUTHOR"];
            var author = _accountService.GetAuthorDashboard(email).Author;
            var list = _accountService.GetCategoryList().Categories;

            try
            {
                if (ModelState.IsValid)
                {
                    _accountService.CreateNewBlogArticle(article, file);

                    return RedirectToAction("Dashboard");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            var model = new NewBlogArticleViewModel
            {
                Author = author,
                Categories = list
            };

            return View(model);
        }
    }
}
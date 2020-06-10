using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TopBloggers.Interfaces.Services;
using TopBloggers.Models;

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
                return HttpNotFound();
            }

            var model = _accountService.GetAuthorDashboard(email);

            return View(model);
        }
    }
}
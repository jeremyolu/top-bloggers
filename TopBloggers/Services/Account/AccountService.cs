using System;
using System.IO;
using System.Linq;
using System.Web;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Models;
using TopBloggers.ViewModels.Account;

namespace TopBloggers.Services.Account
{
    public class AccountService : IAccountService
    {
        private TopBloggersDB _topBloggersDb;

        private IAuthorRepository _authorRepository;
        private ISecurirtyService _securirtyService;

        public AccountService(IAuthorRepository authorRepository, ISecurirtyService securirtyService)
        {
            _authorRepository = authorRepository;
            _securirtyService = securirtyService;
            _topBloggersDb = new TopBloggersDB();
        }

        public Author Login(Author author)
        {
            var encryptedPassword = _authorRepository.GetAuthorByEmail(author.Email).Password;
            var decryptedPassword = _securirtyService.Decrypt(encryptedPassword);

            var user = _topBloggersDb.Authors.SingleOrDefault(a =>
                a.Email == author.Email && decryptedPassword == author.Password);

            return user;
        }

        public Author Register(Author author, HttpPostedFileWrapper file)
        {
            try
            {
                if (file != null)
                {
                    if (IsImage(file.ContentType))
                    {
                        var imageName = Path.GetFileName(file?.FileName);
                        var formattedImgTitle = $"{author.Name}-{author.Surname}-{imageName}";

                        var imagesPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Authors/"), formattedImgTitle);
                        file.SaveAs(imagesPath);

                        author.ProfileImage = formattedImgTitle;
                    }
                }

                author.Password = _securirtyService.Encrypt(author.Password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            _topBloggersDb.Authors.Add(author);
            _topBloggersDb.SaveChanges();

            return author;
        }

        public AuthorAccountViewModel GetAuthorDashboard(string email)
        {
            var author = _authorRepository.GetAuthorByEmail(email);

            var model = new AuthorAccountViewModel
            {
                Author = author
            };

            return model;
        }

        public NewBlogArticleViewModel GetCategoryList()
        {
            var categories = _topBloggersDb.Categories.ToList();

            var model = new NewBlogArticleViewModel
            {
                Categories = categories
            };

            return model;
        }

        public Article CreateNewBlogArticle(Article article, HttpPostedFileWrapper file)
        {
            if (IsImage(file.ContentType))
            {
                var imageName = Path.GetFileName(file.FileName);
                var formattedImgTitle = $"{article.Title}-{imageName}";

                var imagesPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/BlogArticles/"), formattedImgTitle);
                file.SaveAs(imagesPath);

                article.Image = formattedImgTitle;
            }

            article.Likes = 0;
            article.Views = 0;
            article.CreatedDate = DateTime.Today;

            _topBloggersDb.Articles.Add(article);
            _topBloggersDb.SaveChanges();

            return article;
        }

        private bool IsImage(string type)
        {
            return type == "image/jpg" || type == "image/png" || type == "image/jpeg";
        }

    }
}
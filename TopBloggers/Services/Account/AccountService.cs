using System;
using System.Drawing;
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

        public AccountService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _topBloggersDb = new TopBloggersDB();
        }

        public Author Login(Author author)
        {
            var user = _topBloggersDb.Authors.SingleOrDefault(a =>
                a.Email == author.Email && a.Password == author.Password);

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

        private bool IsImage(string type)
        {
            return type == "image/jpg" || type == "image/png" || type == "image/jpeg";
        }

    }
}
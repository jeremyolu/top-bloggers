using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using TopBloggers.Interfaces.Repositories;
using TopBloggers.Interfaces.Services;
using TopBloggers.Repositories.Authors;
using TopBloggers.Repositories.Blogs;
using TopBloggers.Repositories.Categories;
using TopBloggers.Services.Account;
using TopBloggers.Services.Authors;
using TopBloggers.Services.Blogs;
using TopBloggers.Services.Category;

namespace TopBloggers
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Dependency Injection Container

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Services
            builder.RegisterType<BlogService>().As<IBlogService>();
            builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();

            //Repositories
            builder.RegisterType<BlogRepository>().As<IBlogRepository>();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}

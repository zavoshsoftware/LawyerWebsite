using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("")]
        public ActionResult Index()
        {
            HomeViewModel result=new HomeViewModel()
            {
                HomeBlogs = db.Blogs.Where(c=>c.IsDeleted==false&&c.IsActive).OrderByDescending(c=>c.CreationDate).Take(3).ToList()
            };
            return View(result);
        }
        [Route("consultant")]
        public ActionResult Consult()
        {
            ConsultantViewModel result=new ConsultantViewModel()
            {
            };
            return View(result);
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ConsultantViewModel result=new ConsultantViewModel()
            {
            };
            return View(result);
        }
    }
}
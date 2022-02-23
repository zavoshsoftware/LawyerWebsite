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
            HomeViewModel result = new HomeViewModel()
            {
                HomeBlogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c => c.CreationDate).Take(3).ToList(),
                Slides = db.Slides.Where(x => x.IsDeleted == false && x.IsActive).ToList()
            };
            return View(result);
        }
        [Route("consultant")]
        public ActionResult Consult()
        {
            ConsultantViewModel result = new ConsultantViewModel()
            {
            };
            return View(result);
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ConsultantViewModel result = new ConsultantViewModel();
            return View(result);
        }
        [Route("about")]
        public ActionResult About()
        {
            AboutViewModel model = new AboutViewModel();
            model.section1 = new TextItem();
            model.section2 = new TextItem();
            model.section3 = new TextItem();
            model.section1 = db.TextItems.FirstOrDefault(x => x.IsDeleted == false && x.IsActive && x.TextItemType.Name == "AboutUs" && x.Name == "section1");
            model.section1 = db.TextItems.FirstOrDefault(x => x.IsDeleted == false && x.IsActive && x.TextItemType.Name == "AboutUs" && x.Name == "section2");
            model.section1 = db.TextItems.FirstOrDefault(x => x.IsDeleted == false && x.IsActive && x.TextItemType.Name == "AboutUs" && x.Name == "section3");
            return View(model);
        }

    }
}
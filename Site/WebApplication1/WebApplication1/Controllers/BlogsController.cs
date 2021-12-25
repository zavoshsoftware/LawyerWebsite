using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Service).Where(b=>b.IsDeleted==false).OrderByDescending(b=>b.CreationDate);
            return View(blogs.ToList());
        }

        
        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }

                #endregion

                blog.Visit = 0;
                blog.IsDeleted=false;
				blog.CreationDate= DateTime.Now; 
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", blog.ServiceId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", blog.ServiceId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }

                #endregion
                blog.IsDeleted = false;
				blog.LastModifiedDate = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", blog.ServiceId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
			blog.IsDeleted=true;
			blog.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }





        [AllowAnonymous]
        [Route("blog")]
        public ActionResult List()
        {
            BlogListViewModel result = new BlogListViewModel()
            {
                Blogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c => c.CreationDate).ToList()
            };
            return View(result);
        }

        [AllowAnonymous]
        [Route("blog/{urlParam}")]
        public ActionResult Details(string urlParam)
        {
            Blog blog = db.Blogs.FirstOrDefault(c => c.UrlParam == urlParam && c.IsDeleted == false);
            if (blog == null)
            {
                return RedirectToAction("List");
            }

            BlogDetailViewModel result = new BlogDetailViewModel()
            {
                Blog = blog,
                SidebarServices = db.Services.Where(c => c.IsDeleted == false && c.IsActive).OrderBy(c => c.Order).ToList(),
                SidebarBlog = db.Blogs.Where(c => c.IsDeleted == false && c.Id != blog.Id && c.IsActive).OrderByDescending(c => c.CreationDate).Take(4).ToList(),
                BlogComments = db.BlogComments.Where(c=>c.BlogId==blog.Id&&c.IsActive&&c.IsDeleted==false).ToList()
                
            };

            return View(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace WebApplication1.Controllers
{
    public class ServicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Parent).Where(s=>s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Services, "Id", "Title");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,UrlParam,ImageUrl,Summery,Body,Order,IsInHome,ParentId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Service service)
        {
            if (ModelState.IsValid)
            {
				service.IsDeleted=false;
				service.CreationDate= DateTime.Now; 
                service.Id = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Services, "Id", "Title", service.ParentId);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Services, "Id", "Title", service.ParentId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,UrlParam,ImageUrl,Summery,Body,Order,IsInHome,ParentId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Service service)
        {
            if (ModelState.IsValid)
            {
				service.IsDeleted = false;
				service.LastModifiedDate = DateTime.Now;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Services, "Id", "Title", service.ParentId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service service = db.Services.Find(id);
			service.IsDeleted=true;
			service.DeletionDate=DateTime.Now;
 
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
    }
}

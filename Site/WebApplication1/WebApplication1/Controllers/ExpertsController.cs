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

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExpertsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Experts
        public ActionResult Index()
        {
            return View(db.Experts.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: Experts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // GET: Experts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expert expert, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/team/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    expert.ImageUrl = newFilenameUrl;
                }

                #endregion
                expert.IsDeleted=false;
				expert.CreationDate= DateTime.Now; 
                expert.Id = Guid.NewGuid();
                db.Experts.Add(expert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expert);
        }

        // GET: Experts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expert expert, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/team/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    expert.ImageUrl = newFilenameUrl;
                }

                #endregion
                expert.IsDeleted = false;
				expert.LastModifiedDate = DateTime.Now;
                db.Entry(expert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expert);
        }

        // GET: Experts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // POST: Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Expert expert = db.Experts.Find(id);
			expert.IsDeleted=true;
			expert.DeletionDate=DateTime.Now;
 
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

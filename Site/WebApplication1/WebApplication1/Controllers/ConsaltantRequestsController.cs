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
    [Authorize(Roles = "Administrator")]
    public class ConsaltantRequestsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ConsaltantRequests
        public ActionResult Index()
        {
            return View(db.ConsaltantRequests.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: ConsaltantRequests/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsaltantRequest consaltantRequest = db.ConsaltantRequests.Find(id);
            if (consaltantRequest == null)
            {
                return HttpNotFound();
            }
            return View(consaltantRequest);
        }

        // GET: ConsaltantRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsaltantRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Subject,CellNumber,Body,Response,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ConsaltantRequest consaltantRequest)
        {
            if (ModelState.IsValid)
            {
				consaltantRequest.IsDeleted=false;
				consaltantRequest.CreationDate= DateTime.Now; 
                consaltantRequest.Id = Guid.NewGuid();
                db.ConsaltantRequests.Add(consaltantRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consaltantRequest);
        }

        // GET: ConsaltantRequests/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsaltantRequest consaltantRequest = db.ConsaltantRequests.Find(id);
            if (consaltantRequest == null)
            {
                return HttpNotFound();
            }
            return View(consaltantRequest);
        }

        // POST: ConsaltantRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Subject,CellNumber,Body,Response,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ConsaltantRequest consaltantRequest)
        {
            if (ModelState.IsValid)
            {
				consaltantRequest.IsDeleted = false;
				consaltantRequest.LastModifiedDate = DateTime.Now;
                db.Entry(consaltantRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consaltantRequest);
        }

        // GET: ConsaltantRequests/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsaltantRequest consaltantRequest = db.ConsaltantRequests.Find(id);
            if (consaltantRequest == null)
            {
                return HttpNotFound();
            }
            return View(consaltantRequest);
        }

        // POST: ConsaltantRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ConsaltantRequest consaltantRequest = db.ConsaltantRequests.Find(id);
			consaltantRequest.IsDeleted=true;
			consaltantRequest.DeletionDate=DateTime.Now;
 
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
        public ActionResult SubmitRequest(string fullName, string subject, string cellNumber, string email, string message)
        {
            try
            {
                ConsaltantRequest request = new ConsaltantRequest()
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Id = Guid.NewGuid(),
                    CellNumber = cellNumber,
                    IsActive = false,
                    Body = message,
                    FullName = fullName,
                    Email = email,
                    Subject = subject
                };

                db.ConsaltantRequests.Add(request);
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

    }
}

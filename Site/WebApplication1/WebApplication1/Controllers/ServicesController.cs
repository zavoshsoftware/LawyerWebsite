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
    public class ServicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Parent).Where(s=> s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(services.ToList());
        }

        [Route("service/{urlParam}")]
        [AllowAnonymous]
        public ActionResult Details(string urlParam)
        {

            Service service = db.Services.FirstOrDefault(c => c.UrlParam == urlParam);
            if (service == null)
            {
                return HttpNotFound();
            }

            ServiceDetailViewModel result= new ServiceDetailViewModel()
            {
                Service = service,
                SidebarServices = db.Services.Where(c=>c.ParentId==service.ParentId&&c.IsDeleted==false&&c.IsActive).ToList(),
                RelatedBlog = db.Blogs.Where(c=>c.ServiceId==service.Id).ToList()
            };

      
            return View(result);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Services.Where(c=>c.ParentId==null), "Id", "Title");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }
                 
                #endregion
                service.IsDeleted=false;
				service.CreationDate= DateTime.Now; 
                service.Id = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Services.Where(c => c.ParentId == null), "Id", "Title", service.ParentId);
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
            ViewBag.ParentId = new SelectList(db.Services.Where(c => c.ParentId == null), "Id", "Title", service.ParentId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }

                #endregion
                service.IsDeleted = false;
				service.LastModifiedDate = DateTime.Now;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Services.Where(c => c.ParentId == null), "Id", "Title", service.ParentId);
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
        /**************************************************/
        [Route("Services")]
        public ActionResult Services()
        {
            ServicesViewModel model = new ServicesViewModel();
            model.ParentServices = db.Services.Where(x => x.IsDeleted != true && x.ParentId == null).
                ToList();

            foreach (var parentServic in model.ParentServices)
            {
                model.ChildServices.Add(parentServic.Id, db.Services.Where(x => x.IsDeleted == false && x.ParentId == parentServic.Id).ToList());
            }
            return View(model);
        }
        [Route("ServicesByCategory/{urlParam}")]
        public ActionResult ServicesByCategory(string urlParam)
        {
            ServicesByCategoryViewModel model = new ServicesByCategoryViewModel();
            model.ParentService = new Service();
            model.ParentService = db.Services.FirstOrDefault(x => x.IsDeleted == false && x.ParentId == null && x.UrlParam == urlParam);
            var id = model.ParentService.Id;
            model.ChildServices = db.Services.Include(p => p.Parent).Where(x => x.IsDeleted != true && x.ParentId == id).ToList();

            return View(model);
        }
        /**********************************************************************/
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


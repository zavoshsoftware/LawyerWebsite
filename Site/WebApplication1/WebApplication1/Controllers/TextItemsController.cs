﻿using System;
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
    public class TextItemsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TextItems
        public ActionResult Index()
        {
            var textItems = db.TextItems.Include(t => t.TextItemType).Where(t=>t.IsDeleted==false).OrderByDescending(t=>t.CreationDate);
            return View(textItems.ToList());
        }

        // GET: TextItems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            return View(textItem);
        }

        // GET: TextItems/Create
        public ActionResult Create()
        {
            ViewBag.TextItemTypeId = new SelectList(db.TextItemTypes, "Id", "Title");
            return View();
        }

        // POST: TextItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Name,ImageUrl,Summery,Body,LinkUrl,LinkTitle,TextItemTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] TextItem textItem)
        {
            if (ModelState.IsValid)
            {
				textItem.IsDeleted=false;
				textItem.CreationDate= DateTime.Now; 
                textItem.Id = Guid.NewGuid();
                db.TextItems.Add(textItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TextItemTypeId = new SelectList(db.TextItemTypes, "Id", "Title", textItem.TextItemTypeId);
            return View(textItem);
        }

        // GET: TextItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TextItemTypeId = new SelectList(db.TextItemTypes, "Id", "Title", textItem.TextItemTypeId);
            return View(textItem);
        }

        // POST: TextItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Name,ImageUrl,Summery,Body,LinkUrl,LinkTitle,TextItemTypeId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] TextItem textItem)
        {
            if (ModelState.IsValid)
            {
				textItem.IsDeleted = false;
				textItem.LastModifiedDate = DateTime.Now;
                db.Entry(textItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TextItemTypeId = new SelectList(db.TextItemTypes, "Id", "Title", textItem.TextItemTypeId);
            return View(textItem);
        }

        // GET: TextItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            return View(textItem);
        }

        // POST: TextItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TextItem textItem = db.TextItems.Find(id);
			textItem.IsDeleted=true;
			textItem.DeletionDate=DateTime.Now;
 
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

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using _3.Helpers;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Entities.Items;
using _3.DAL;
using PagedList;

namespace _3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemInfoesController : Controller
    {
        private MyDbContext db = new MyDbContext();
        readonly string[] ExcludedFields = new string[] {};

        // GET: ItemInfoes
        public ActionResult Index(string sortOrder, int? page, string sortProperty = "ID")
        {
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortProperty = sortProperty;
            ViewBag.properties = typeof(ItemInfo).GetProperties().Where(p => Array.IndexOf(ExcludedFields, p.Name) == -1).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.ItemInfo.ToList().SortByProperty(sortOrder, sortProperty).ToPagedList(pageNumber, pageSize));
        }

        // GET: ItemInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemInfo itemInfo = db.ItemInfo.Find(id);
            if (itemInfo == null)
            {
                return HttpNotFound();
            }
            return View(itemInfo);
        }

        // GET: ItemInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HPModifier,Size,Name,Description,DropChance,MaxDropCount")] ItemInfo itemInfo)
        {
            if (ModelState.IsValid)
            {
                db.ItemInfo.Add(itemInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemInfo);
        }

        // GET: ItemInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemInfo itemInfo = db.ItemInfo.Find(id);
            if (itemInfo == null)
            {
                return HttpNotFound();
            }
            return View(itemInfo);
        }

        // POST: ItemInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HPModifier,Size,Name,Description,DropChance,MaxDropCount")] ItemInfo itemInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemInfo);
        }

        // GET: ItemInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemInfo itemInfo = db.ItemInfo.Find(id);
            if (itemInfo == null)
            {
                return HttpNotFound();
            }
            return View(itemInfo);
        }

        // POST: ItemInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemInfo itemInfo = db.ItemInfo.Find(id);
            db.ItemInfo.Remove(itemInfo);
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

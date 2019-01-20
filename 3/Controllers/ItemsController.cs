using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Entities;
using _3.DAL;
using _3.Helpers;
using PagedList;

namespace _3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemsController : DefaultController
    {
        readonly string[] ExcludedFields = new string[] { "ItemInfo"};

        // GET: Items
        public ActionResult Index(string sortOrder, int? page, string sortProperty = "ID")
        {
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortProperty = sortProperty;
            ViewBag.properties = typeof(Item).GetProperties().Where(p => Array.IndexOf(ExcludedFields, p.Name) == -1).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Db.Item.ToList().SortByProperty(sortOrder, sortProperty).ToPagedList(pageNumber, pageSize));
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = Db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForbiddenFields = new string[] { };
            ViewBag.Controller = "Item";
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                Db.Item.Add(item);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = Db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(item).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = Db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = Db.Item.Find(id);
            Db.Item.Remove(item);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}

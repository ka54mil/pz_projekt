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

namespace _3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PocketsController : DefaultController
    {
        // GET: Pockets
        public ActionResult Index()
        {
            var pocket = db.Pocket.Include(p => p.Being).Include(p => p.Item);
            return View(pocket.ToList());
        }

        // GET: Pockets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pocket pocket = db.Pocket.Find(id);
            if (pocket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForbiddenFields = new string[] { };
            ViewBag.Controller = "Pocket";
            return View(pocket);
        }

        // GET: Pockets/Create
        public ActionResult Create()
        {
            ViewBag.BeingID = new SelectList(db.Hero, "ID", "ID");
            ViewBag.ItemID = new SelectList(db.Item, "ID", "ID");
            return View();
        }

        // POST: Pockets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Quantity,ItemID,BeingID")] Pocket pocket)
        {
            if (ModelState.IsValid)
            {
                db.Pocket.Add(pocket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeingID = new SelectList(db.Hero, "ID", "ID", pocket.BeingID);
            ViewBag.ItemID = new SelectList(db.Item, "ID", "ID", pocket.ItemID);
            return View(pocket);
        }

        // GET: Pockets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pocket pocket = db.Pocket.Find(id);
            if (pocket == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeingID = new SelectList(db.Hero, "ID", "ID", pocket.BeingID);
            ViewBag.ItemID = new SelectList(db.Item, "ID", "ID", pocket.ItemID);
            return View(pocket);
        }

        // POST: Pockets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Quantity,ItemID,BeingID")] Pocket pocket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pocket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeingID = new SelectList(db.Hero, "ID", "ID", pocket.BeingID);
            ViewBag.ItemID = new SelectList(db.Item, "ID", "ID", pocket.ItemID);
            return View(pocket);
        }

        // GET: Pockets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pocket pocket = db.Pocket.Find(id);
            if (pocket == null)
            {
                return HttpNotFound();
            }
            return View(pocket);
        }

        // POST: Pockets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pocket pocket = db.Pocket.Find(id);
            db.Pocket.Remove(pocket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

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
    public class EffectsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Effects
        public ActionResult Index()
        {
            return View(db.Effect.ToList());
        }

        // GET: Effects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Effect effect = db.Effect.Find(id);
            if (effect == null)
            {
                return HttpNotFound();
            }
            return View(effect);
        }

        // GET: Effects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Effects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Duration,Power,BeingID,EffectType")] Effect effect)
        {
            if (ModelState.IsValid)
            {
                db.Effect.Add(effect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(effect);
        }

        // GET: Effects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Effect effect = db.Effect.Find(id);
            if (effect == null)
            {
                return HttpNotFound();
            }
            return View(effect);
        }

        // POST: Effects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Duration,Power,BeingID,EffectType")] Effect effect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(effect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(effect);
        }

        // GET: Effects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Effect effect = db.Effect.Find(id);
            if (effect == null)
            {
                return HttpNotFound();
            }
            return View(effect);
        }

        // POST: Effects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Effect effect = db.Effect.Find(id);
            db.Effect.Remove(effect);
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

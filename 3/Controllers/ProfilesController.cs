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
    public class ProfilesController : DefaultController
    {
        readonly string[] ExcludedFields = new string[] { "Being" };
        // GET: Profiles
        public ActionResult Index(string sortOrder, int? page, string sortProperty = "ID")
        {
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortProperty = sortProperty;
            ViewBag.properties = typeof(Profile).GetProperties().Where(p => Array.IndexOf(ExcludedFields, p.Name) == -1).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Db.Profile.ToList().SortByProperty(sortOrder, sortProperty).ToPagedList(pageNumber, pageSize));
        }

        // GET: Profiles/Details/5
        public ActionResult Details(int? id, string sortOrder, string sortProperty = "ID")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = Db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForbiddenFields = new string[] { };
            ViewBag.Controller = "Profile";
            profile.Heroes = profile.Heroes?.ToList().SortByProperty(sortOrder, sortProperty);
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                Db.Profile.Add(profile);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = Db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(profile).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = Db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = Db.Profile.Find(id);
            Db.Profile.Remove(profile);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

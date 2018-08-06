﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Entities;
using _3.DAL;
using System.Reflection;
using _3.Helpers;
using _3.Models;
using PagedList;

namespace _3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HeroesController : Controller
    {
        private MyDbContext db = new MyDbContext();
        readonly string[] ExcludedFields = new string[] { "Profiles", "Pockets", "Effects" };

        // GET: Heroes
        public ActionResult Index(string sortOrder, HeroSearchModel searchModel,int? page, string sortProperty = "ID")
        {
            var Heroes = db.Hero.Include(h => h.Profiles).ToList();
            Heroes = Heroes.SortByProperty(sortOrder, sortProperty).SearchByProperties(searchModel);
            ViewBag.searchModel = searchModel;
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortProperty = sortProperty;
            ViewBag.properties = typeof(Hero).GetProperties().Where(p => Array.IndexOf(ExcludedFields, p.Name) == -1).ToList();
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(Heroes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Heroes/Details/5
        public ActionResult Details(int? id, string sortOrder, string sortProperty = "ID")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Hero.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExcludedFields = ExcludedFields;
            ViewBag.ModelName = "Hero";
            hero.Pockets = hero.Pockets.ToList().SortByProperty(sortOrder, sortProperty);
            return View(hero);
        }

        // GET: Heroes/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profile, "ID", "UserName");
            return View();
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,MHP,AHP,MMP,AMP,Lvl,Exp,Str,Dex,Sta,Int,Spd,MinDmg,MaxDmg,PhysRes,Class,Race,MaxPockets,ProfileID")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                db.Hero.Add(hero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profile, "ID", "UserName", hero.ProfileID);
            return View(hero);
        }

        // GET: Heroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Hero.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profile, "ID", "UserName", hero.ProfileID);
            return View(hero);
        }

        // POST: Heroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,MHP,AHP,MMP,AMP,Lvl,Exp,Str,Dex,Sta,Int,Spd,MinDmg,MaxDmg,PhysRes,Class,Race,MaxPockets,ProfileID")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profile, "ID", "UserName", hero.ProfileID);
            return View(hero);
        }

        // GET: Heroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.Hero.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }

            ViewBag.ExcludedFields = ExcludedFields;
            ViewBag.ModelName = "Hero";
            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hero hero = db.Hero.Find(id);
            db.Hero.Remove(hero);
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

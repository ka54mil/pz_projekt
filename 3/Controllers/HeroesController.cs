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
using System.Reflection;
using _3.Helpers;
using _3.Models;
using PagedList;

namespace _3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class HeroesController : DefaultController
    {
        readonly string[] ExcludedFields = new string[] { "Profile", "Pockets", "Effects", "Weapon" };

        // GET: Heroes
        public ActionResult Index(string sortOrder, HeroSearchModel searchModel,int? page, string sortProperty = "ID")
        {
            var Heroes = Db.Hero.Include(h => h.Profile).ToList();
            Heroes = Heroes.SortByProperty(sortOrder, sortProperty).SearchByProperties(searchModel);
            ViewBag.searchModel = searchModel;
            ViewBag.sortOrder = sortOrder;
            ViewBag.sortProperty = sortProperty;
            var excludedFields = ExcludedFields.Concat(new string[] { "WeaponLvl", "Str", "Spd", "Sta", "Dex", "Int", "AMP", "AHP", "MMP", "MHP", "MinDmg", "MaxDmg", "Exp", "PhysRes", "ExpToLvlUp", "Gold" }).ToArray();
            ViewBag.properties = typeof(Hero).GetProperties().Where(p => Array.IndexOf(excludedFields, p.Name) == -1).ToList();
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
            Hero hero = Db.Hero.Find(id);
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
            ViewBag.ProfileID = new SelectList(Db.Profile, "ID", "UserName");
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
                Db.Hero.Add(hero);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(Db.Profile, "ID", "UserName", hero.ProfileID);
            return View(hero);
        }

        // GET: Heroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = Db.Hero.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(Db.Profile, "ID", "UserName", hero.ProfileID);
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
                Db.Entry(hero).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(Db.Profile, "ID", "UserName", hero.ProfileID);
            return View(hero);
        }

        // GET: Heroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = Db.Hero.Find(id);
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
            Hero hero = Db.Hero.Find(id);
            Db.Hero.Remove(hero);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

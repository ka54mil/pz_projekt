using _3.DAL;
using _3.Helpers;
using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3.Controllers
{
    [Authorize]
    public class GamePlayController : DefaultController
    { 
        // GET: GamePlay
        public ActionResult Index()
        {
            return View();
        }

        // GET: GamePlay/Choose
        public ActionResult ChooseCharacter()
        {
            Profile Profile = db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var Heroes = db.Hero.Where(h => h.ProfileID == Profile.ID).ToList();
            Heroes = Heroes.SortByProperty("desc", "LastPlayedAt");
            return View(Heroes);
        }

        public ActionResult Play(int id)
        {
            Hero hero = db.Hero.Find(id);
            return View(hero);
        }


        // GET: GamePlay/Create
        public ActionResult CreateCharacter()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCharacter([Bind(Include = "Name, Class, Race")] Hero hero)
        {
            hero.InitializeStats();
            Profile profile = db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            hero.ProfileID = profile.ID;
            ModelState.Clear();
            TryValidateModel(hero);
            if (ModelState.IsValid)
            {
                db.Hero.Add(hero);
                db.SaveChanges();
                return RedirectToAction("ChooseCharacter");
            }
          
            return View(hero);
        }
    }
}
using _3.DAL;
using _3.Helpers;
using ClassLibrary.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace _3.Controllers
{
    [Authorize]
    public class GameplayController : DefaultController
    {
        // GET: GamePlay
        public ActionResult Index()
        {
            return View();
        }

        // GET: GamePlay/Choose
        public ActionResult ChooseCharacter()
        {
            Profile Profile = Db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var Heroes = Db.Hero.Where(h => h.ProfileID == Profile.ID).ToList();
            Heroes = Heroes.SortByProperty("desc", "LastPlayedAt");
            return View(Heroes);
        }

        public ActionResult Play(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("ChooseCharacter");
            }
            Hero hero = Db.Hero.Find(id);
            hero.LastPlayedAt = DateTime.UtcNow;
            Db.Entry(hero).State = EntityState.Modified;
            Db.SaveChanges();
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
            Monster mouse = new Monster { Name = "Mouse", MHP = 1, Exp = 1, EncounterChance = 50 };

            Profile profile = Db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            hero.ProfileID = profile.ID;
            ModelState.Clear();
            TryValidateModel(hero);
            if (ModelState.IsValid)
            {
                Db.Hero.Add(hero);
                Db.SaveChanges();
                return RedirectToAction("ChooseCharacter");
            }
          
            return View(hero);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCharacter(int heroId)
        {
            Hero hero = Db.Hero.Find(heroId);
            Profile profile = Db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if(hero == null || hero.ProfileID != profile.ID)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Db.Hero.Remove(hero);
            if(0 == Db.SaveChanges())
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET: GamePlay/ExecuteAction
        [HttpPost]
        public String ExecuteAction(String action, int heroId)
        {
            Hero hero = Db.Hero.Find(heroId);
            Gameplay gameplay = RedisContext.GetFromRedis<Gameplay>($"gameplay-{heroId}");

            if (gameplay == null)
            {
                gameplay = new Gameplay(hero);
            }

            string result = new GameplayActions(gameplay).ExecuteAction(action);
            if (!RedisContext.SaveToRedis($"gameplay-{gameplay.Player.ID}", gameplay))
            {
                return "There was an error while saving game.";
            }
            return result;
        }
    }
}
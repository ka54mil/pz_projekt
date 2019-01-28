﻿using _3.DAL;
using _3.Helpers;
using _3.ViewModels;
using ClassLibrary.Entities;
using ClassLibrary.Exceptions;
using ClassLibrary.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Threading.Tasks;
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
            Profile profile = Db.Profile.FirstOrDefault(u => u.UserName == User.Identity.Name);
            hero.InitializeStats();
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
            if (0 == Db.SaveChanges())
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            RedisContext.Remove($"hero-gamesave-{heroId}");
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET: GamePlay/ExecuteAction
        [HttpPost]
        public JsonResult ExecuteAction(String action, int heroId)
        {
            Hero hero = Db.Hero.Find(heroId);
            Gameplay gameplay = RedisContext.GetByKey<Gameplay>($"hero-gamesave-{heroId}") ?? new Gameplay(hero);
            JsonResult jsonResult = new JsonResult();
            GameplayModel gameplayModel;
            gameplay.Player = hero;
            try
            {
                List<string> result = new GameplayActions(gameplay).ExecuteAction(action).ToList();
                if (0 == gameplay.Monsters.Count)
                {
                    gameplay.Monsters = gameplay.CurrentLocation.AmbushPlayer();
                    result.AddRange(gameplay.Monsters.Select(m => $"You have been attacked by {m.Name}"));
                }
                gameplayModel = new GameplayModel(hero, result.ToArray());
                if (!RedisContext.Save($"hero-gamesave-{gameplay.Player.ID}", gameplay))
                {
                    gameplayModel.Messages = new String[] { "There was an error while saving game." };
                }
                else
                {
                    hero.Pockets.ToList().ForEach(x => {
                        Db.Entry(x.Item.ItemInfo).State = EntityState.Modified;
                    });
                    hero.LastPlayedAt = DateTime.UtcNow;
                    Db.Entry(hero).State = EntityState.Modified;
                    Db.SaveChanges();
                }

            }
            catch (InvalidActionException e)
            {
                gameplayModel = new GameplayModel(hero, new string[] { e.Message });

            }
            jsonResult.Data = StringHelper.SerializeObject(gameplayModel);

            return jsonResult;
        }
    }
}
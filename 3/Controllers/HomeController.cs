using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3.DAL;
using _3.ViewModels;

namespace _3.Controllers
{
    [Authorize]
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<HeroesLevels> data = from Hero in Db.Hero
                                                   group Hero by Hero.Lvl into LvlGroup
                                                   select new HeroesLevels()
                                                   {
                                                       Lvl = LvlGroup.Key,
                                                       Count = LvlGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult LoadIcon()
        {
            HttpPostedFileBase file = Request.Files["Icon"];
            if(file != null && file.ContentLength > 0)
            {
                file.SaveAs(HttpContext.Server.MapPath(SiteModel.getLogoIconFolder())+ SiteModel.getLogoIcon());
            }
            return View(new SiteModel());
        }
    }
}

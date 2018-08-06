using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3.Controllers
{
    [Authorize]
    public class GamePlayController : Controller
    {
        // GET: GamePlay
        public ActionResult Index()
        {
            return View();
        }
    }
}
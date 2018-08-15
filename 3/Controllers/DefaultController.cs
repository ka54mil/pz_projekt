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
    public class DefaultController : Controller
    {
        public MyDbContext db { get; } = new MyDbContext();
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

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
using ServiceStack.Redis;
using Newtonsoft.Json;

namespace _3.Controllers
{
    public class DefaultController : Controller
    {
        public MyDbContext Db { get; } = new MyDbContext();
        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}

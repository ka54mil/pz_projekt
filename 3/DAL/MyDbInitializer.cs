﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using _3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ClassLibrary.Entities;
using ClassLibrary.Entities.Items;
using ClassLibrary.Entities.Items.Food;

namespace _3.DAL
{
    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext())
                );
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())
                );

            roleManager.Create(new IdentityRole("Admin"));

            var user = new ApplicationUser { UserName = "Cos@Cos.Cos" };
            string password = "CosCos";
            userManager.Create(user, password);
            userManager.AddToRole(user.Id, "Admin");
            context.Profile.Add(new Profile { UserName = user.UserName });
            context.ItemInfo.Add(new Meat());
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
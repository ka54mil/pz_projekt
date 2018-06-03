using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3.DAL
{
    public class MyDbContext : DbContext
    {
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Pocket> Pocket { get; set; }
        public DbSet<Hero> Hero { get; set; }
        public DbSet<Effect> Effect { get; set; }
        public MyDbContext() : base("DefaultConnection")
        {
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
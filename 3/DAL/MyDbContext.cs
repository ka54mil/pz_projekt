using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
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
            modelBuilder.Entity<Hero>()
                .Property(h => h.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            base.OnModelCreating(modelBuilder);
        }
    }
}
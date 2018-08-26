using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        public override int MinDmg { get { return Weapon.MinDmg + base.MinDmg; } set { base.MinDmg = value; } }
        public override int MaxDmg { get { return Weapon.MaxDmg + base.MaxDmg; } set { base.MaxDmg = value; } }
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }
        public virtual Profile Profile { get; set; }
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Last time played")]
        public DateTime? LastPlayedAt { get; set; }
        public Weapon Weapon { get; set; }

        public Hero()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override void InitializeStats()
        {
            base.InitializeStats();
            Weapon = new Weapon("Fists", 0, 5,0,1);
        }

        public void AddWeaponStats()
        {
            MinDmg += Weapon.MinDmg;

        }
    }
}
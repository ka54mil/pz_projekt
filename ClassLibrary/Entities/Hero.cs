using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ClassLibrary.Helpers;
using Newtonsoft.Json;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        public new int MinDmg {
            get {
                Weapon = Weapon == null ? new Weapon(WeaponLvl) : Weapon;
                return Weapon.MinDmg + base.MinDmg;
            }
            set {
                int weaponDmg = Weapon == null ? 0 : Weapon.MinDmg;
                base.MinDmg = value - Weapon.MinDmg;
            }
        }
        public new int MaxDmg
        {
            get
            {
               Weapon = Weapon == null ? new Weapon(WeaponLvl) : Weapon;
                return Weapon.MaxDmg + base.MaxDmg;
            }
            set
            {
                int weaponDmg = Weapon == null ? 0 : Weapon.MaxDmg;
                base.MaxDmg = value - Weapon.MaxDmg;
            }
        }
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Last time played")]
        public DateTime? LastPlayedAt { get; set; }
        public int WeaponLvl { get; set; }
        public virtual Profile Profile { get; set; }
        [NotMapped]
        public virtual Weapon Weapon { get; set; }

        public Hero()
        {
            Weapon = new Weapon(WeaponLvl);
            CreatedAt = DateTime.UtcNow;
        }

        public override void InitializeStats()
        {
            base.InitializeStats();
        }

        public void KillEnemy(Monster enemy)
        {
            base.KillEnemy(enemy);
            //AddItemsToPockets(RandomHelper.GetDroppedItems(enemy));
        }
    }
}
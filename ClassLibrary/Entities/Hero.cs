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
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Last time played")]
        public DateTime? LastPlayedAt { get; set; }
        private int _weaponLvl;
        [Display(Name = "Weapon level")]
        [Range(0, maxValue, ErrorMessage = "Weapon level can't be negative")]
        public int WeaponLvl { get => _weaponLvl; set { Weapon = new Weapon(value); _weaponLvl = value; } }
        public virtual Profile Profile { get; set; }
        [NotMapped]
        public virtual Weapon Weapon { get; set; }

        public new int MinDmg
        {
            get
            {
                Weapon = Weapon ?? new Weapon(WeaponLvl);
                return Weapon.MinDmg + base.MinDmg;
            }
            set
            {
                base.MinDmg = value - (Weapon == null ? 0 : Weapon.MinDmg);
            }
        }
        public new int MaxDmg
        {
            get
            {
                Weapon = Weapon ?? new Weapon(WeaponLvl);
                return Weapon.MaxDmg + base.MaxDmg;
            }
            set
            {
                base.MaxDmg = value - (Weapon == null ? 0 : Weapon.MaxDmg);
            }
        }

        public Hero()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override int GetDmg()
        {
            Random r = new Random();
            return r.Next(MinDmg, MaxDmg + 1);
        }

        public override void InitializeStats()
        {
            base.InitializeStats();
        }

        public bool IsWeapongUpgradeble()
        {
            return Gold >= Weapon.UpgradeCost;
        }

        public bool TryUpgradeWeapon()
        {
            if (IsWeapongUpgradeble())
            {
                Gold -= Weapon.UpgradeCost;
                WeaponLvl = Weapon.Lvl;
                return true;
            }
            return false;
        }

        public void KillEnemy(Monster enemy)
        {
            base.KillEnemy(enemy);
            //AddItemsToPockets(RandomHelper.GetDroppedItems(enemy));
        }
    }
}
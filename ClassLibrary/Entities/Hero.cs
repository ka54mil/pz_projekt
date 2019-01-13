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
        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Max pockets")]
        public int MaxPockets { get; set; }

        public virtual ICollection<Pocket> Pockets { get; set; }
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
            MaxPockets = 10;
            CreatedAt = DateTime.UtcNow;
        }

        public override int GetDmg()
        {
            Random r = new Random();
            return 100;// r.Next(MinDmg, MaxDmg + 1);
        }

        public override void InitializeStats()
        {
            base.InitializeStats();
            MaxPockets = 10;
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
                Weapon.Upgrade(1);
                WeaponLvl = Weapon.Lvl;
                return true;
            }
            return false;
        }

        public void KillEnemy(Monster enemy)
        {
            base.KillEnemy(enemy);
        }

        public void AddItemsToPockets(List<Item> items)
        {
            items.ForEach(i => AddItemToPocket(i));
        }

        public void AddItemToPocket(Item item)
        {
            Pocket pocket = Pockets.Where(p => item.ItemInfo.Name.Equals(p.Item.ItemInfo.Name) && p.Item.ItemInfo.Size <= Pocket.MaxPocketSize - p.Item.ItemInfo.Size*p.Item.Quantity)
                .FirstOrDefault();
            
            if (Object.Equals(pocket, null))
            {
                Pockets.Add(new Pocket { Being = this, Item = item});
            }
            else
            {
                while (Pocket.MaxPocketSize - pocket.Item.ItemInfo.Size * pocket.Item.Quantity >= item.ItemInfo.Size && item.Quantity>0)
                {
                    pocket.Item.Quantity++;
                    item.Quantity--;
                }
                if (item.Quantity > 0)
                {
                    Pockets.Add(new Pocket { Being = this, Item = item });
                }
            }
        }
    }
}
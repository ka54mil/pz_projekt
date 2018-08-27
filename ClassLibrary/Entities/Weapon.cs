using ClassLibrary.Generators;
using ClassLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [NotMapped]
    public class Weapon : Item
    {
        public int UpgradeCost { get; set; }
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public int Lvl { get; set; }

        public Weapon()
        {

        }
        public Weapon(int lvl)
        {
            Weapon weapon = ItemGenerator.GetWeaponByLvl(lvl);
            UpgradeCost = weapon.UpgradeCost;
            MinDmg = weapon.MinDmg;
            MaxDmg = weapon.MaxDmg;
            Lvl = weapon.Lvl;
            Name = weapon.Name;
            while (lvl > Lvl)
            {
                Upgrade();
            }
        }

        public void Upgrade()
        {
            MinDmg += Lvl;
            MaxDmg = MinDmg + Lvl / 3;
            UpgradeCost += Lvl * Lvl + 3;
        }
    }
}

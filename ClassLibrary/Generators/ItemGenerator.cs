using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Generators
{
    internal static class ItemGenerator
    {
        public static Weapon[] weapons =
        {
            new Weapon{Name = "Fists", MaxDmg = 1, UpgradeCost =3}
        };

        public static Weapon GetWeaponByLvl(int lvl)
        {
            return lvl >= weapons.Length ? weapons[weapons.Length - 1] : weapons[lvl];
        }
    }
}

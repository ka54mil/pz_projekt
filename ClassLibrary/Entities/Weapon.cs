using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Weapon : Item
    {
        public int UpgradeCost { get; set; }
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public Weapon()
        {

        }
        public Weapon(string name, int size,int upgradeCost, int minDmg, int maxDmg) : base(name, size)
        {
            UpgradeCost = upgradeCost;
            MinDmg = minDmg;
            MaxDmg = maxDmg;
        }
    }
}

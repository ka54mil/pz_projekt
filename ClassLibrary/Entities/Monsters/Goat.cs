using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities.Monsters
{
    public class Goat : Monster
    {
        public Goat()
        {
            Name = "Goat";
            MHP = 3;
            Exp = 5;
            Gold = 5;
            Sta = 2;
            Str = 1;
            Dex = 1;
            MinDmg = 1;
            MaxDmg = 3;
            Spd = 2;
            EncounterChance = 2000000;
        }
    }
}

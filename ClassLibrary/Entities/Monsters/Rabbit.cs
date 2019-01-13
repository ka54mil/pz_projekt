using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities.Monsters
{
    public class Rabbit : Monster
    {
        public Rabbit()
        {
            Name = "Rabbit";
            MHP = 1;
            Exp = 2;
            Gold = 2;
            Sta = 1;
            MaxDmg = 1;
            Spd = 1;
            EncounterChance = 2500000;
        }
    }
}

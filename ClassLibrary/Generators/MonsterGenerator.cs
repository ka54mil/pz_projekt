﻿using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Generators
{
    internal static class MonsterGenerator
    {
        internal static List<Monster> CreateMonsterList(LocationType locationType)
        {
            List<Monster> monsters = new List<Monster>();

            Monster mouse = new Monster { Name = "Mouse", Exp = 1, Gold = 1, EncounterChance=30};
            mouse.InitializeStats();
            Monster rabbit = new Monster { Name = "Rabbit", MHP = 1, Exp = 2, Gold = 2, Sta = 1, MaxDmg = 1, Spd = 1,EncounterChance = 25 };
            rabbit.InitializeStats();
            Monster goat = new Monster { Name = "Goat", MHP = 3, Exp = 5, Gold = 5, Sta = 2, Str = 1,Dex=1 ,MinDmg=1 , MaxDmg = 3, Spd = 2, EncounterChance=20 };
            goat.InitializeStats();

            switch (locationType)
            {
                case LocationType.Plain_field:
                    monsters.Add(mouse);
                    monsters.Add(rabbit);
                    break;
                case LocationType.Forest:
                    monsters.Add(rabbit);
                    break;
                case LocationType.Mountain:
                    monsters.Add(goat);
                    break;
            }
            return monsters;
        }
    }
}

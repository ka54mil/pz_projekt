﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Monster : Being, ICloneable
    {
        public int EncounterChance { get; set; }
        public Monster()
        {

        }

        public override void InitializeStats()
        {
            Str += 1;
            Dex += 1;
            Int += 1;
            Spd += 1;
            Sta += 1;
            MaxPockets += 5;
            Lvl += 1;
            MinDmg += 1;
            MaxDmg += MinDmg;
            Class = Class.Unknown;
            Race = Race.Unknown;
            PhysRes += (Sta) / 3;
            MHP += Sta * 2;
            MMP += Int;
            AHP = MHP;
            AMP = MMP;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
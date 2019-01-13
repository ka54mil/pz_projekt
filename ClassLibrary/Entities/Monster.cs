using ClassLibrary.Entities.Items;
using ClassLibrary.Entities.Items.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    [NotMapped]
    public class Monster : Being
    {
        public int EncounterChance { get; set; }
        public Item[] Items { get; set; }
        public Monster()
        {
            Items = new Item[] { new Item { ItemInfoID = 1, ItemInfo = new Meat() } };
        }

        public override void InitializeStats()
        {
            Str += 1;
            Dex += 1;
            Int += 1;
            Spd += 1;
            Sta += 1;
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
    }
}
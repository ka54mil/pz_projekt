using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Entities
{
    public class Being : Entity
    {
        public int ID { get; set; }
        public int MHP { get; set; }
        public int AHP { get; set; }
        public int MMP { get; set; }
        public int AMP { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Sta { get; set; }
        public int Int { get; set; }
        public int Spd { get; set; }
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public int PhysRes { get; set; }
        public Class Class { get; set; }
        public Race Race { get; set; }
        public int MaxPockets { get; set; }
        public virtual ICollection<Effect> Effects { get; set; }
        public virtual ICollection<Pocket> Pockets { get; set; }

    }

    public enum Race
    {
        Unknown,
        Human,
        Beast,
        Undead,
        Elf
    }

    public enum Class
    {
        Unknown,
        Warrior,
        Archer,
        Mage
    }
}
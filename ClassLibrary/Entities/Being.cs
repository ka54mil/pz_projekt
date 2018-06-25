using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ClassLibrary.Entities
{
    public class Being : Entity
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]*", ErrorMessage = "Only numbers and letters are allowed")]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Maximum health points")]
        public int MHP { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Actual health points")]
        public int AHP { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Maximum mana points")]
        public int MMP { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Actual mana points")]
        public int AMP { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Level")]
        public int Lvl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Experience")]
        public int Exp { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Strength")]
        public int Str { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Dexterity")]
        public int Dex { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Stamina")]
        public int Sta { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Inteligence")]
        public int Int { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Speed")]
        public int Spd { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Min damage")]
        public int MinDmg { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Max damage")]
        public int MaxDmg { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Physical resistance")]
        public int PhysRes { get; set; }
        
        [Display(Name = "Class")]
        public Class Class { get; set; }
        
        [Display(Name = "Race")]
        public Race Race { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Max pockets")]
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
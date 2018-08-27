using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ClassLibrary.Entities
{
    public partial class Being : Entity
    {
        protected const int maxValue = int.MaxValue;

        [Display(Name = "ID")]
        public int ID { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]*", ErrorMessage = "Only numbers, spaces and letters are allowed")]
        [Display(Name = "Name")]
        [Required]
        [StringLength(30,ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 4)]
        public String Name { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Maximum health points")]
        public int MHP { get; set; }

        [Range(0, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Actual health points")]
        public int AHP { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Maximum mana points")]
        public int MMP { get; set; }

        [Range(0, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name="Actual mana points")]
        public int AMP { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Level")]
        public int Lvl { get; set; }

        [Range(0, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Experience")]
        public int Exp { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Strength")]
        public int Str { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Dexterity")]
        public int Dex { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Stamina")]
        public int Sta { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Inteligence")]
        public int Int { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Speed")]
        public int Spd { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Min damage")]
        public int MinDmg { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Max damage")]
        public int MaxDmg { get; set; }

        [Display(Name = "Physical resistance")]
        public int PhysRes { get; set; }
        
        [Display(Name = "Class")]
        [Range(1, maxValue, ErrorMessage = "Class can not be unknown")]
        public Class Class { get; set; }
        
        [Display(Name = "Race")]
        [Range(1, maxValue, ErrorMessage = "Race can not be unknown")]
        public Race Race { get; set; }

        [Range(1, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Max pockets")]
        public int MaxPockets { get; set; }

        [Range(0, maxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Experience to level up")]
        public int ExpToLvlUp { get; set; }

        [Range(0, maxValue, ErrorMessage = "Only positive number allowed")]
        public int Gold { get; set; }

        public virtual ICollection<Effect> Effects { get; set; }

        public virtual ICollection<Pocket> Pockets { get; set; }

        public virtual void InitializeStats()
        {
            InitializeStats(1);
        }
        public virtual void InitializeStats(int lvl)
        {
            Str = 1;
            Dex = 1;
            Int = 1;
            Spd = 1;
            Sta = 1;
            MaxPockets = 10;
            MinDmg = 1;
            MaxDmg = MinDmg;
            MHP = 1;
            MMP = 1;
            LvlUp(lvl);
        }
    }

    public enum Race
    {
        Unknown,
        Human,
        Elf,
        Beast,
    }

    public enum Class
    {
        Unknown,
        Mage,
        Archer,
        Warrior,
    }
}
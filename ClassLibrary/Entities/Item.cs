using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Item : Entity
    {
        public int ID { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public ItemType ItemType { get; set; }
    }

    public enum ItemType
    {
        Unknown,
        Weapon,
        Armor,
        Ring,
        Necklace,
        Boots,
        Shield,
        Helmet,
        Gloves,
        Trousers,
        Consumable
    }
}
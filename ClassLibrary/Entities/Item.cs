using ClassLibrary.Entities.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Item : Entity
    {
        public int ID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int ItemInfoID { get; set; }
        public virtual ItemInfo ItemInfo { get; set; }
        public Item()
        {

        }

        public Item(string name):this(name, 1)
        {
        }

        public Item(string name, int size)
        {
            ItemInfo.Name = name;
            ItemInfo.Size = size;
        }
        public void Use(Being target)
        {
            target.ChangeAHP(ItemInfo.HPModifier);
        }
    }
}
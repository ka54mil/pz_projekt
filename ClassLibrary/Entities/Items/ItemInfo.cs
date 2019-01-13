using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities.Items
{
    public class ItemInfo : Entity
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        public int HPModifier { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DropChance { get; set; }
        public int MaxDropCount { get; set; }
    }
}

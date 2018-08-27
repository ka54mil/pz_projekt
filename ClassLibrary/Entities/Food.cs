using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Food : Item, IDroppable
    {
        public int Size { get; set; }
        public int DropChance { get; set; }
        public int MaxDropCount { get; set; }
    }
}

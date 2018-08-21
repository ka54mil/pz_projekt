using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Location : Entity
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Description { get; set; }
    }
}

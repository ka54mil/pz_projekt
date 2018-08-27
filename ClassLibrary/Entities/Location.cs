using ClassLibrary.Helpers;
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
        public List<Monster> Monsters { get; set; } = new List<Monster>();
        public LocationType LocationType { get; set; }
        public List<LocationType> UnlockedByLocationTypes { get; set; } = new List<LocationType>();
        public int MaxAmbushSize { get; set; }

        public List<Monster> AmbushPlayer()
        {
            return RandomHelper.GetAmbushByMonsters(Monsters, MaxAmbushSize);
        }
    }

    public enum LocationType
    {
        Unknown,
        Plain_field,
        Forest,
        Mountain
    }
}

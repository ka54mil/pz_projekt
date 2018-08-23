using ClassLibrary.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class World : Entity
    {
        public List<Location> Locations { get; set; } = new List<Location>();

        internal Location AddLocation(Location location)
        {
            Locations.Add(location);
            return location;
        }

        internal Location LocationAt(int x, int y)
        {
            foreach (Location l in Locations)
            {
                if (l.X == x && l.Y == y)
                {
                    return l;
                }
            }
            
            return WorldGenerator.CreateLocationAt(this, x, y);
        }
    }
}

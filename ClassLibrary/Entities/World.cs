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
        internal void AddLocation(string name, int x, int y,string description)
        {
            Location location = new Location {X=x, Y=y, Name=name, Description=description };
            Locations.Add(location);
        }

        public Location LocationAt(int x, int y)
        {
            foreach (Location l in Locations)
            {
                if (l.X == x && l.Y == y)
                {
                    return l;
                }
            }

            return null;
        }
    }
}

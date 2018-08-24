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
        public List<LocationType> UnclockedByLocationTypes { get; set; } = new List<LocationType>();
        
        public List<Monster> AmbushPlayer()
        {
            List<Monster> monsters = new List<Monster>();
            Random r = new Random();
            foreach(Monster m in Monsters)
            {
                var tmpEncounterChance = m.EncounterChance;
                int i = 1;
                while(r.Next(100) < m.EncounterChance){
                    Monster clone = m.Clone() as Monster;
                    clone.Name = $"{clone.Name} {i}";
                    monsters.Add(clone);
                    m.EncounterChance = (m.EncounterChance+4) / 5*4;
                }
                m.EncounterChance = tmpEncounterChance;
            };
            return monsters;
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

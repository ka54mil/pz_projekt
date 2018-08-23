using ClassLibrary.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    [Serializable]
    public class Gameplay : Entity
    {
        public Hero Player { get; set; }
        public World World { get; set; }
        public Location CurrentLocation { get; set; }
        public List<Monster> Monsters { get; set; }

        public Gameplay(Hero player)
        {
            Player = player;
            Monsters = new List<Monster>();
            World = WorldGenerator.CreateWorld();
            CurrentLocation = World.LocationAt(0, 0);
        }
    }
}

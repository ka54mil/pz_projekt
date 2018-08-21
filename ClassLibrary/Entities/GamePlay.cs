using ClassLibrary.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Gameplay
    {
        public Hero Player { get; set; }
        public World World { get; set; }
        public Location CurrentLocation { get; set; }

        public Gameplay(Hero player)
        {
            Player = player;
            WorldFactory factory = new WorldFactory();
            World = factory.CreateWorld();
            CurrentLocation = World.LocationAt(0, 0);
        }
    }
}

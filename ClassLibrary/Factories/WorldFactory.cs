using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Factories
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World world = new World();

            world.AddLocation("Start", 0, 0, "Starting location");
            world.AddLocation("Plain field", 1, 0, "Full of mice and rabbits");
            world.AddLocation("Forest", 2, 0, "You can hear wolves and bears");
            world.AddLocation("Plain field", 0, 1, "Full of mice and rabbits");
            world.AddLocation("Plain field", 0, -1, "Full of mice and rabbits");
            world.AddLocation("Plain field", -1, 0, "Full of mice and rabbits");

            return world;
        }
    }
}

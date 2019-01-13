using ClassLibrary.Entities;
using ClassLibrary.Entities.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Generators
{
    internal static class MonsterGenerator
    {
    
        internal static Monster mouse = new Mouse();
        internal static Monster rabbit = new Rabbit();
        internal static Monster goat = new Goat();

        static MonsterGenerator(){
            mouse.InitializeStats();
            rabbit.InitializeStats();
            goat.InitializeStats();
        }

        internal static List<Monster> CreateMonsterList(LocationType locationType)
        {
            List<Monster> monsters = new List<Monster>();

            switch (locationType)
            {
                case LocationType.Plain_field:
                    monsters.Add(mouse);
                    monsters.Add(rabbit);
                    break;
                case LocationType.Forest:
                    monsters.Add(rabbit);
                    break;
                case LocationType.Mountain:
                    monsters.Add(goat);
                    break;
            }
            return monsters;
        }
    }
}

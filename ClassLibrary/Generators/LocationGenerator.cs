using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Generators
{
    internal static class LocationGenerator
    {
        internal static Location[] locationTemplates = new Location[] {
                new Location {
                    Name = "Plain field",
                    Description = "Full of mice and rabbits",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Plain_field),
                    LocationType = LocationType.Plain_field
                },
                new Location{
                    Name = "Forest",
                    Description = "You can hear wolves and bears",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Forest),
                    LocationType = LocationType.Forest
                },
                new Location{
                    Name = "Mountain",
                    Description = "Be aware of snakes",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Mountain),
                    LocationType = LocationType.Mountain
                }
            };
        internal static Location CreateLocation(int x, int y)
        {
            Random r = new Random();
            int i = r.Next(locationTemplates.Length);
            Location location = locationTemplates[i];
            location.X = x;
            location.Y = y;
            return location;
        }
    }
}

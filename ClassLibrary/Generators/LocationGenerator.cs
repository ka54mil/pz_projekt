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
                    LocationType = LocationType.Plain_field,
                    UnclockedByLocationTypes = new List<LocationType>{LocationType.Unknown}
                },
                new Location{
                    Name = "Forest",
                    Description = "You can hear wolves and bears",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Forest),
                    LocationType = LocationType.Forest,
                    UnclockedByLocationTypes = new List<LocationType>{LocationType.Plain_field}
                },
                new Location{
                    Name = "Mountain",
                    Description = "Be aware of snakes",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Mountain),
                    LocationType = LocationType.Mountain,
                    UnclockedByLocationTypes = new List<LocationType>{LocationType.Forest}
                }
            };
        internal static Location CreateUnlockedLocation(List<LocationType> UnlockedLocationTypes, int x, int y)
        {
            Random r = new Random();
            Location[] tmpLocationTemplates = locationTemplates.Where(
                l => UnlockedLocationTypes.Intersect(l.UnclockedByLocationTypes).Count() == l.UnclockedByLocationTypes.Count()
            ).ToArray();
            int i = r.Next(tmpLocationTemplates.Length);
            Location location = tmpLocationTemplates[i];
            location.X = x;
            location.Y = y;
            return location;
        }
    }
}

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
                    Description = "Full of mice and rabbits.",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Plain_field),
                    LocationType = LocationType.Plain_field,
                    UnlockedByLocationTypes = new List<LocationType>{LocationType.Unknown},
                    MaxAmbushSize = 1
                },
                new Location{
                    Name = "Forest",
                    Description = "You can hear wolves and bears.",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Forest),
                    LocationType = LocationType.Forest,
                    UnlockedByLocationTypes = new List<LocationType>{LocationType.Plain_field},
                    MaxAmbushSize = 2
                },
                new Location{
                    Name = "Mountain",
                    Description = "Be aware of snakes.",
                    Monsters = MonsterGenerator.CreateMonsterList(LocationType.Mountain),
                    LocationType = LocationType.Mountain,
                    UnlockedByLocationTypes = new List<LocationType>{LocationType.Forest},
                    MaxAmbushSize = 3
                }
            };
        internal static Location CreateUnlockedLocation(List<LocationType> UnlockedLocationTypes, int x, int y)
        {
            Random r = new Random();
            var tmpLocationTemplates = locationTemplates.Where(
                l => UnlockedLocationTypes.Intersect(l.UnlockedByLocationTypes).Count() == l.UnlockedByLocationTypes.Count()
            );
            int i = r.Next(tmpLocationTemplates.Count());
            Location location = tmpLocationTemplates.ElementAt(i);
            location.X = x;
            location.Y = y;
            return location;
        }
    }
}

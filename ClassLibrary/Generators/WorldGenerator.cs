using ClassLibrary.Entities;
using System;

namespace ClassLibrary.Generators
{
    internal static class WorldGenerator
    {        
        internal static World CreateWorld()
        {
            World world = new World();
            world.AddLocation(new Location() { Name="Start", X=0, Y=0, Description="Starting location", LocationType = LocationType.Unknown});
            return world;
        }

        internal static Location CreateLocationAt(World world, int x, int y)
        {
            return world.AddLocation(LocationGenerator.CreateLocation(x, y));
        }
    }
}

using ClassLibrary.Generators;
using ClassLibrary.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public delegate void PlayerDeathHandler();
    public class Gameplay : Entity
    {
        public event PlayerDeathHandler OnPlayerDeath;
        [JsonIgnore]
        public Hero Player { get; set; }
        public World World { get; set; }
        public Location CurrentLocation { get; set; }
        public List<Monster> Monsters { get; set; }

        public Gameplay()
        {
            OnPlayerDeath += PlayerDeath;
        }
        public Gameplay(Hero player)
        {
            Player = player;
            Monsters = new List<Monster>();
            World = WorldGenerator.CreateWorld();
            CurrentLocation = World.LocationAt(0, 0);
            OnPlayerDeath += PlayerDeath;
        }

        public Monster GetMonsterByName(string name)
        {
            name = StringHelper.ToUppercaseFirst(name);
            return Monsters.Where(e => e.Name.Equals(name)).FirstOrDefault() ?? Monsters.FirstOrDefault();
        }

        private void PlayerDeath()
        {
            CurrentLocation = World.LocationAt(0, 0);
            Monsters.Clear();
            Player.Revive();
        }
        public void KillPlayer()
        {
            OnPlayerDeath();
        }
    }
}

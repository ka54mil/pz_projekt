using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers
{
    public static class RandomHelper
    {
        public static List<Monster> GetAmbushByMonsters(List<Monster> Monsters, int maxCount)
        {
            List<Monster> monsters = new List<Monster>();
            Random r = new Random();
            foreach (Monster m in Monsters)
            {
                var tmpEncounterChance = m.EncounterChance;
                m.EncounterChance *= 100;
                int i = 1;
                while (r.Next(10000) < m.EncounterChance && monsters.Count < maxCount)
                {
                    Monster clone = m.Clone<Monster>();
                    clone.Name = $"{clone.Name} {i}";
                    monsters.Add(clone);
                    m.EncounterChance = (m.EncounterChance + 4) / 5 * 4;
                }
                m.EncounterChance = tmpEncounterChance;
            };
            return monsters;
        }
        public static List<Item> GetDroppedItems(Monster Monster)
        {
            List<Item> items = new List<Item>();
            Random r = new Random();
            foreach (IDroppable i in Monster.GetDroppableItems())
            {
                var tmpDropChance = i.DropChance;
                i.DropChance *= 100;
                while (r.Next(10000) < i.DropChance && items.Count < i.MaxDropCount)
                {
                    Item clone = (i as Item).Clone<Item>();
                    items.Add(clone);
                    i.DropChance = (i.DropChance + 5) / 6 * 5;
                }
                i.DropChance = tmpDropChance;
            };
            return items;
        }
    }
}

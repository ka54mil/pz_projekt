using ClassLibrary.Entities;
using ClassLibrary.Entities.Items;
using ClassLibrary.Entities.Items.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers
{
    public static class RandomHelper
    {
        public static List<Monster> GetAmbushByMonsters(List<Monster> monsters, int maxCount)
        {
            List<Monster> ambush = new List<Monster>();
            Random r = new Random();
            foreach (Monster m in monsters)
            {
                int tmpEncounterChance = m.EncounterChance;
                int i = 1;
                while (r.Next(10000000) < tmpEncounterChance && ambush.Count < maxCount)
                {
                    Monster clone = m.Clone<Monster>();
                    clone.Name = $"{clone.Name} {i++}";
                    ambush.Add(clone);
                    tmpEncounterChance = (int) ((tmpEncounterChance) * Math.Pow(4.0/5.0, ambush.Count)) ;
                }
            };
            return ambush;
        }
        public static List<Item> GetDroppedItems(Monster Monster)
        {
            List<Item> items = new List<Item>();
            Random r = new Random();
            foreach (var i in Monster.Items)
            {
                int tmpDropChance = i.ItemInfo.DropChance;
                //i.ItemInfo = i.ItemInfo as Meat;
                while (r.Next(10000000) < tmpDropChance && i.Quantity < i.ItemInfo.MaxDropCount)
                {
                    i.Quantity++;
                    tmpDropChance = (int)((tmpDropChance)* Math.Pow(5.0 / 6.0, i.Quantity));
                }
                if (i.Quantity> 0)
                {
                    items.Add(i);
                }
            };
            return items;
        }
    }
}

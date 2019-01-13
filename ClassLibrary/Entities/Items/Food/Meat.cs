using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities.Items.Food
{
    public class Meat : FoodInfo
    {
        public Meat()
        {
            ID = 1;
            Size = 1;
            Name = "Meat";
            Description = $"Eat to restore {HPModifier} hp";
            HPModifier = 4;
            DropChance = 9000000;
            MaxDropCount = 1;
    }
    }
}

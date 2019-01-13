using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities.Monsters
{
    public class Mouse : Monster
    {
        public Mouse()
        {
            Name = "Mouse";
            Exp = 1;
            Gold = 1;
            EncounterChance = 3000000;
        }
    }
}

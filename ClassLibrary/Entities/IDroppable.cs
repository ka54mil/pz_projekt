using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IDroppable
    {
        int Size { get; set; }
        int DropChance { get; set; }
        int MaxDropCount { get; set; }
    }
}

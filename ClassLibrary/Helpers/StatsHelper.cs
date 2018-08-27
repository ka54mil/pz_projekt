using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers
{
    public static class StatsHelper
    {
        public static int GetRecalculatedStatValue(int dividor, int currentValue)
        {
            return GetRecalculatedStatValue(dividor, currentValue, currentValue - 1);
        }

        public static int GetRecalculatedStatValue(int dividor, int currentValue, int previousValue)
        {
            return (currentValue + previousValue) / dividor - (currentValue + previousValue) / dividor;
        }
    }
}

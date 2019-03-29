using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    class IndicatorOverTime
        //holds result from indicator and time indicator was built (depending upon bar data, timeframe dependent)

    {
        public decimal result { get; set; }
        public DateTime time { get; set; }


    }
}

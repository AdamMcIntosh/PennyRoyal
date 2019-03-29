using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    public class Candle
    {
        public bool complete { get; set; }
        public int volume { get; set; }
        public DateTime time { get; set; }
        public Mid mid { get; set; }
    }

}


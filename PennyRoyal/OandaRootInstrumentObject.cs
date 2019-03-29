using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
namespace PennyRoyal
{
    public class OandaRootInstrumentObject
    {
        public string instrument { get; set; }
        public string granularity { get; set; }
        public List<Candle> candles { get; set; }
                
    }
}

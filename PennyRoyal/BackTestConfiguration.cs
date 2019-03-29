using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    class BackTestConfiguration
    {
        public string timeFrame { get; set; }
        public string currencyPair { get; set; }
        public int maxTradesPerDay { get; set; }
        public DateTime startDate { get; set; }
        public int runLength { get; set; }
        public decimal startingBalance { get; set; }
        public decimal riskPercent { get; set; }
        public decimal riskVsReward { get; set; }
        public string logFile { get; set; }
        public string connectionString { get; set; }
        
    }
}

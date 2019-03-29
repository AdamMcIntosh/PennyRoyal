using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    class LogData
        //holds log data to write to csv

    {
        public DateTime SystemTime { get; set; }
        public DateTime BarTime { get; set; }
        public bool BarComplete { get; set; }
        public decimal BarOpen { get; set; }
        public decimal BarHigh { get; set; }
        public decimal BarLow { get; set; }
        public decimal BarClose { get; set; }
        public decimal Volume { get; set; }
        public decimal SMA50 { get; set; }
        public TrendDirection  Trend { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal StandardDeviation { get; set; }
        public decimal Distance { get; set; }
        public string Trade { get; set; }
        public int ProfitTrades { get; set; }
        public int LossTrades { get; set; }
        public int TotalTrades { get; set; }
    }
}

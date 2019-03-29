using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    public class Account
    {
        public string alias { get; set; }
        public decimal balance { get; set; }
        public string createdByUserID { get; set; }
        public DateTime createdTime { get; set; }
        public string currency { get; set; }
        public int lastTransactionID { get; set; }
        public decimal marginAvailable { get; set; }
        public decimal marginRate {get; set;}    
        public decimal marginUsed { get; set; }
        public int openPositionCount { get; set; }
        public int openTradeCount { get; set; }
        public decimal  pl { get; set; }
        public decimal positionValue { get; set; }
        public List<object> orders { get; set; }
        public List<object> positions { get; set; }
        public List<object> trades { get; set; }

    }
}

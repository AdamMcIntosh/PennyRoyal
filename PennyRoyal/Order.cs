using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    public class Order
    {
        public decimal units { get; set; }
        public string instrument { get; set; }
        public string id { get; set; }
        public DateTime createTime { get; set; }
        public string type { get; set; }
        public string tradeID { get; set; }
        public string price { get; set; }       //really think this should be decimal, but weird json serialization issue when sending as decimal
        public bool guaranteed { get; set; }
        public string timeInForce { get; set; }
        public string triggerCondition { get; set; }
        public string state { get; set; }
        
    }
}

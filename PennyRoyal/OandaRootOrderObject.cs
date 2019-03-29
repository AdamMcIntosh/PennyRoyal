using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    //used to receive order data from endpoint

    public class OandaRootOrderObject
    {
        public List<Order> orders { get; set; }
        public string lastTransactionID { get; set; }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    //needed to submit order, because body of json must have "order" before order data

    class PostOrder
    {
        public Order order { get; set; }


    }
}

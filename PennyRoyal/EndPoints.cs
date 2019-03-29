using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    static class EndPoints
    {
        public static string accounts(){return "/accounts"; }
        public static string instruments(){ return "/instruments"; }
        public static string order(string ID) { return string.Format("/accounts/{0}/orders", ID); }
    };

}


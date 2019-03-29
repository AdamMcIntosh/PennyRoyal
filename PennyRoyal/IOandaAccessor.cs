using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Timers;
namespace PennyRoyal
{
    public interface IOandaAccessor
    {
        //interface for accessing Oanda endpoints, includes subscriber for polling event
        IRestResponse response { get; set; }
        void OnUpdate(object sender, ElapsedEventArgs  e);
        void RegisterTimer(System.Timers.Timer timer);
        AccountSettings accountSettings { get; set; }
    }
}

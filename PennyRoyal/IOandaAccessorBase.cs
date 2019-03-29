using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Timers;
namespace PennyRoyal
{
    public abstract class IOandaAccessorBase : IOandaAccessor
    {
        public abstract AccountSettings accountSettings { get; set; }

        private IRestResponse _response { get; set; }
        public IRestResponse response
            {
            get { return _response; }
            set { _response = value; }


            }
        virtual public void OnUpdate(object _sender, ElapsedEventArgs _e){}
        virtual public void RegisterTimer(System.Timers.Timer _timer){}



    }
}

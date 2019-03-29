using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System.Timers;
namespace PennyRoyal
{
    public class AccountAccessor : IOandaAccessor
    {
        public IRestResponse response { get; set; }
        public AccountSettings accountSettings { get; set; }
        private DateTime startTime;
        private OandaRootAccountObject data;



        public decimal GetBalance()
        {
            return this.data.account.balance;
        }

        public AccountAccessor(AccountSettings _accountSettings)
        {

            this.accountSettings = _accountSettings;
            
            this.startTime = DateTime.Now;

            var client = new RestClient(string.Format("{0}{01}/{02}", accountSettings.BASEURI, EndPoints.accounts(), accountSettings.ID));
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            request.RequestFormat = DataFormat.Json;
            IRestResponse<OandaRootAccountObject> _response = client.Execute<OandaRootAccountObject>(request);
            this.response = _response;
            data = _response.Data;
            //Program.mainTimer.Elapsed += this.OnUpdate;

            /*
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account Status:{0}", this.response.StatusCode);
            Console.WriteLine("Start  time:{0}",startTime);
            Console.WriteLine("Starting Balance:{0}", data.account.balance);
            Console.ResetColor();
            */    
    }
        public void OnUpdate(object _sender, ElapsedEventArgs _e)
        {
            var client = new RestClient(string.Format("{0}{01}/{02}", accountSettings.BASEURI, EndPoints.accounts(), accountSettings.ID));
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            request.RequestFormat = DataFormat.Json;
            IRestResponse<OandaRootAccountObject> _response = client.Execute<OandaRootAccountObject>(request);
            this.data = _response.Data;  
            this.response = _response;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current Balance:{0}", data.account.balance);
            Console.WriteLine("Current system time:{0}", DateTime.Now);
            Console.ResetColor();

        }
        public void RegisterTimer(Timer _timer)
        {
            _timer.Elapsed += this.OnUpdate;
        }



    }
}

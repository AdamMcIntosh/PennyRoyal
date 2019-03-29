using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
namespace PennyRoyal
{
    public class OrderAccessor
    {
        public IRestResponse response { get; set; }
        public AccountSettings accountSettings { get; set; }
        private PostOrder marketOrder;
        private PostOrder stopLoss;
        private PostOrder takeProfit;

        public OrderAccessor(AccountSettings _settings)
            {

            this.accountSettings = _settings;
            var client = new RestClient(string.Format("{0}{1}", accountSettings.BASEURI, EndPoints.order(accountSettings.ID)));
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            request.RequestFormat = DataFormat.Json;
            IRestResponse _response = client.Execute(request);

            /*
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Order Status:{0}", _response.StatusCode);
            Console.ResetColor();
            */        
            }

        public  OandaRootOrderObject SubmitOrder(string _instrument, decimal _profitTarget, decimal _stopTarget, decimal _positionSize)
        {
            //market order

            var clientMarket = new RestClient(string.Format("{0}{1}", accountSettings.BASEURI, EndPoints.order(accountSettings.ID)));
            RestRequest marketRequest = new RestRequest() { Method = Method.POST };
            marketRequest.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            marketRequest.AddHeader("Content-Type", "application/json");
            marketRequest.RequestFormat = DataFormat.Json;
            this.marketOrder = new PostOrder();
            this.marketOrder.order = new Order() { instrument = _instrument, units = _positionSize, type = "MARKET",timeInForce="FOK", triggerCondition="DEFAULT" };
            marketRequest.AddBody(this.marketOrder);
            IRestResponse<OandaRootOrderObject> marketResponse = clientMarket.Execute<OandaRootOrderObject>(marketRequest);

            var transactionID = marketResponse.Data.lastTransactionID;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Market Order ID:{0}", transactionID);
            Console.ResetColor();
            //see oanda documentation http://developer.oanda.com/rest-live-v20/order-df/

            //stop loss
            
            var clientStopLoss = new RestClient(string.Format("{0}{1}", accountSettings.BASEURI, EndPoints.order(accountSettings.ID)));
            RestRequest stopLossRequest = new RestRequest() { Method = Method.POST };
            stopLossRequest.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            stopLossRequest.AddHeader("Content-Type", "application/json");
            stopLossRequest.RequestFormat = DataFormat.Json;
            this.stopLoss = new PostOrder();
                        
            this.stopLoss.order = new Order() { tradeID = transactionID, price = Convert.ToString(_stopTarget), type = "STOP_LOSS", timeInForce = "GTC", triggerCondition = "DEFAULT" };
            stopLossRequest.AddBody(this.stopLoss);
            IRestResponse<OandaRootOrderObject> stopLossResponse = clientStopLoss.Execute<OandaRootOrderObject>(stopLossRequest);
            
            //take profit
            var clientTakeProfit = new RestClient(string.Format("{0}{1}", accountSettings.BASEURI, EndPoints.order(accountSettings.ID)));
            RestRequest takeProfitRequest = new RestRequest() { Method = Method.POST };
            takeProfitRequest.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            takeProfitRequest.AddHeader("Content-Type", "application/json");
            takeProfitRequest.RequestFormat = DataFormat.Json;
            this.takeProfit = new PostOrder();
            this.takeProfit.order = new Order() { tradeID = transactionID, price = Convert.ToString(_profitTarget), type = "TAKE_PROFIT", timeInForce = "GTC", triggerCondition="DEFAULT" };
            takeProfitRequest.AddBody(this.takeProfit);
            IRestResponse<OandaRootOrderObject> takeProfitResponse = clientTakeProfit.Execute<OandaRootOrderObject>(takeProfitRequest);
            

            return marketResponse.Data;
        }

    }
}

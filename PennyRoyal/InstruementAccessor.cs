using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Timers;
namespace PennyRoyal
{
    public class InstrumentAccessor :IOandaAccessor
    {
        public AccountSettings accountSettings { get; set; }
        public IRestResponse response { get; set; }
        private OandaRootInstrumentObject bar;  //holds temp bar object, also used if not adding to master collection
        private List<OandaRootInstrumentObject> allBars;    //hold all bars across different time frames
        const string defaultTimeFrame = "M5";   //default timeframe 5 minute chart
        const int defaultPeriod = 1;
        private string timeFrame = defaultTimeFrame;        
        private int period = defaultPeriod;
        private string currencyPair;

        private OandaRootInstrumentObject OandaGetBars(DateTime _startTime, DateTime _endTime)
        {
            //get bars time range from Oanda endpoint
            {

                //really we have to convert to unix epoch BS?
                DateTimeOffset startOffset = new DateTimeOffset(_startTime);
                DateTimeOffset endOffset = new DateTimeOffset(_endTime);
                
                var client = new RestClient(string.Format("{0}{1}/{2}/candles", accountSettings.BASEURI, EndPoints.instruments(), this.currencyPair));
                RestRequest request = new RestRequest() { Method = Method.GET };
                request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("from", startOffset.ToUnixTimeSeconds());
                request.AddParameter("to",endOffset.ToUnixTimeSeconds());
                request.AddParameter("granularity", this.timeFrame);
                
                IRestResponse<OandaRootInstrumentObject> _response = client.Execute<OandaRootInstrumentObject>(request);
                if (_response.Data.candles.Count() == 0)
                {
                    throw new ProgramException(string.Format("no bars returned from end point:{0}",_startTime.ToString()));
                    
                }

                return _response.Data;
            }
        }

        private OandaRootInstrumentObject  OandaGetBars()
        //get bars from Oanda endpoint
        {

            var client = new RestClient(string.Format("{0}{1}/{2}/candles", accountSettings.BASEURI, EndPoints.instruments(), this.currencyPair));
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            request.AddParameter("count", this.period);
            request.AddParameter("granularity", this.timeFrame);  
            request.RequestFormat = DataFormat.Json;
            IRestResponse<OandaRootInstrumentObject> _response = client.Execute<OandaRootInstrumentObject>(request);
            if (_response.Data.candles.Count() == 0)
            {
                throw new ProgramException("no bars returned from end point");
            }

            return _response.Data;
        }
        public InstrumentAccessor(AccountSettings _accountSettings, string _currencyPair)
        {
            this.currencyPair = _currencyPair;
            this.accountSettings=_accountSettings;                        
            var client = new RestClient(string.Format("{0}{1}/{2}/candles", accountSettings.BASEURI, EndPoints.instruments(),this.currencyPair));
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddHeader("Authorization", string.Format("Bearer {0}", accountSettings.APIKEY));
            request.AddParameter("count", this.period);
            request.AddParameter("granularity",this.timeFrame);  //5 minute chart
            request.RequestFormat = DataFormat.Json;
            IRestResponse<OandaRootInstrumentObject> _response = client.Execute<OandaRootInstrumentObject>(request);
            //this.data = _response.Data;  //set public field to deserialized data
            this.bar = _response.Data;
            this.allBars = new List<OandaRootInstrumentObject>();
            this.allBars.Add(new OandaRootInstrumentObject { instrument = this.currencyPair, granularity = this.timeFrame, candles = new List<Candle>() });
            this.allBars[0].candles.Add(this.bar.candles[0]);

            this.response = _response;


            //Program.mainTimer.Elapsed += this.OnUpdate;  //add Onupdate method to timer invocation list

            /*
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Instrument Status:{0}", _response.StatusCode);
            Console.WriteLine("Using {0} currency pair", this.currencyPair);
            Console.ResetColor();
            */


        }

        
        public void OnUpdate(object  _sender, ElapsedEventArgs  _e)
        //get latest single bar

        {
            this.period = defaultPeriod;
            this.timeFrame = defaultTimeFrame;

            this.bar = OandaGetBars();

            //find whichever object is the default time frame
            int index=FindTimeFrame(this.timeFrame);

            if (this.bar.candles[0].time > this.allBars[index].candles[this.allBars[index].candles.Count - 1].time && (bar.candles[0].complete=true) )
            //if time of current bar is greater then time of last stored bar and bar is complete
            {
                this.allBars[index].candles.Add(this.bar.candles[0]);
                Console.WriteLine("Currency:{0}   Time:{1}", this.currencyPair, this.allBars[index].candles[this.allBars[index].candles.Count - 1].time);
                Console.WriteLine("Volume:{0}", this.allBars[index].candles[this.allBars[index].candles.Count - 1].volume);
                Console.WriteLine("Open:{0}", this.allBars[index].candles[this.allBars[index].candles.Count - 1].mid.o);
                Console.WriteLine("High:{0}", this.allBars[index].candles[this.allBars[index].candles.Count - 1].mid.h);
                Console.WriteLine("Close:{0}", this.allBars[index].candles[this.allBars[index].candles.Count - 1].mid.c);
                Console.WriteLine("Low:{0}", this.allBars[index].candles[this.allBars[index].candles.Count - 1].mid.l);
                
            }


            
        }

        public void RegisterTimer(Timer _timer)
        {
            _timer.Elapsed+= OnUpdate; 
        }
        //Program.mainTimer.Elapsed += this.OnUpdate;

        private int FindTimeFrame(string _timeFrame)
        //returns object from allbars list that matches requested timeframe
        //if the current timeframe isn't stored yet, create it
        {
            //need to make sure list doesn't have multiple entries for any given time frame

            IEnumerable<OandaRootInstrumentObject> query = from result in this.allBars where result.granularity == _timeFrame select result;
            if (query.Count() > 1)
            {
                throw new ProgramException("multiple entries for time frame are present");
            }
            else if (query.Count() > 0)
            {
                return this.allBars.FindIndex(i => i.granularity == _timeFrame);

            }

            this.allBars.Add(new OandaRootInstrumentObject() { instrument = this.currencyPair, granularity = _timeFrame, candles = new List<Candle>() });
            return this.allBars.FindIndex(i => i.granularity == _timeFrame);
        }


        public OandaRootInstrumentObject GetBars(DateTime _startTime, DateTime _endTime, string _timeFrame)
        {
            timeFrame = _timeFrame;
            bar = OandaGetBars(_startTime, _endTime);
            return bar;
        }

        public  OandaRootInstrumentObject GetBars(int _barsReturn, string _timeFrame)    
        //return amount of bars for requested timeframe
        //need this later to get bar history from other classes
        {
            this.period = _barsReturn;
            this.timeFrame = _timeFrame;
            int index = FindTimeFrame(_timeFrame);

            if (_barsReturn > this.allBars[index].candles.Count) //if we are asking for more bars then stored, query endpoint and return bars, otherwise use currently stored data
            {
                //reintialize collection and return result

                this.bar=OandaGetBars();
                this.allBars[index] = new OandaRootInstrumentObject() { instrument = this.currencyPair, granularity = this.timeFrame, candles = new List<Candle>() };
                this.allBars[index].candles = this.bar.candles;
                return this.allBars[index]; 
                
            }
            else
            {
                //return subset of allBars collection
                this.bar.candles = this.allBars[index].candles.GetRange((this.allBars[index].candles.Count - 1 - _barsReturn), _barsReturn);
                return this.bar;  
        
            }

        }

    }
}

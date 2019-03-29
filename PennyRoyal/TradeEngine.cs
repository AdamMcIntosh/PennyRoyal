using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using CsvHelper;
namespace PennyRoyal
{
    sealed class TradeEngine
    {
        private const string BASEURI = "";
        private const string APIKEY = "";
        private const string ID = "";
        private const string outFile = "TradeEngineLog.csv";
        private System.Timers.Timer mainTimer;
        private List<IndicatorOverTime> SMA50;
        private AccountSettings myAccountSettings;
        private AccountAccessor myAccount;
        private InstrumentAccessor instruments;
        private OrderAccessor orders;
        private LogData tradeEngineLog;
        private TrendDirection trend;
        private void WriteLog()
        {
            //build log

            tradeEngineLog =new LogData
            {
                SystemTime = DateTime.Now,
                BarTime = instruments.GetBars(1, "M5").candles[0].time,
                BarOpen = instruments.GetBars(1, "M5").candles[0].mid.o,
                BarComplete = instruments.GetBars(1,"M5").candles[0].complete,
                BarClose = instruments.GetBars(1, "M5").candles[0].mid.c,
                BarHigh = instruments.GetBars(1, "M5").candles[0].mid.h,
                BarLow = instruments.GetBars(1, "M5").candles[0].mid.l,
                Volume = instruments.GetBars(1, "M5").candles[0].volume,
                SMA50 = SMA50[SMA50.Count - 1].result,
                Trend = trend
            };

            //write to csv file

            using (StreamWriter writer = new StreamWriter(outFile, append:true))
            {
                CsvWriter csvWriter = new CsvWriter(writer);
                csvWriter.WriteRecord(tradeEngineLog);
                csvWriter.NextRecord();
                
            }
            


        }
        public TradeEngine()
        {
            //create main timer for polling
            mainTimer = new System.Timers.Timer();
            mainTimer.Interval = 150000;         //launch polling event every 2.5 minutes
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;

            myAccountSettings = new AccountSettings { BASEURI = BASEURI, APIKEY = APIKEY, ID = ID };

            //initialize default variables
            myAccount = new AccountAccessor(myAccountSettings);
            myAccount.RegisterTimer(mainTimer);

            instruments = new InstrumentAccessor(myAccountSettings, "EUR_USD");
            instruments.RegisterTimer(mainTimer);

            orders = new OrderAccessor(myAccountSettings);

            //OMG, initialize  first object in collection
            SMA50 = new List<IndicatorOverTime>();
            SMA50.Add(new IndicatorOverTime{ result = Indicators.SMA((instruments.GetBars(50, "M5"))), time = instruments.GetBars(1, "M5").candles[0].time });


            trend = new TrendDirection();

            //set because log file is written before trend is actually calculated, so should be sideways until there is enough data to actually determine the trend
            trend = TrendDirection.Sideways;

            tradeEngineLog = new  LogData();

            //write headers to csv
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                CsvWriter csvWriter = new CsvWriter(writer);
                csvWriter.WriteHeader<LogData>();
                csvWriter.NextRecord();

            }



            WriteLog();
            mainTimer.Elapsed += OnUpdate;

        }

    public void OnUpdate(object _sender, ElapsedEventArgs _e)
        {

            //check if new data needs to be built from bar data, and if so write log file
            if (instruments.GetBars(1,"M5").candles[0].time>SMA50[SMA50.Count-1].time)
            {
                SMA50.Add(new IndicatorOverTime { result = Indicators.SMA((instruments.GetBars(50, "M5"))), time = instruments.GetBars(1, "M5").candles[0].time });
                trend = Indicators.Trend(SMA50);
                WriteLog();
            }
            
                    
            

            //Console.WriteLine("Simple Moving average over 50 bars:{0}", SMA50[SMA50.Count - 1].result);
            //Console.WriteLine("The current trend is:{0}",trend);




        }


    }
}

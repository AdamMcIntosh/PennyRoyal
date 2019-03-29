using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using ShellProgressBar;
using MathNet;
using System.Data.SqlClient;
using System.Linq.Dynamic;
using System.Data.Linq;
namespace PennyRoyal
{
    class BackTest
        //backtest before porting to TradeEngine

    {
        private const string BASEURI = "";
        private const string APIKEY = "";
        private const string ID = "";
        private List<IndicatorOverTime> indicator;
        private AccountSettings myAccountSettings;
        private AccountAccessor myAccount;
        private InstrumentAccessor instruments;
        private OrderAccessor orders;
        private LogData tradeEngineLog;
        private TrendDirection trend;
        private OandaRootInstrumentObject bars;
        private List<double> closingPrice;
        private int runLength;
        private decimal standardDeviation;
        private decimal takeProfit;
        private decimal stopLoss;
        public int profitTrades;
        public int lossTrades;
        public int totalTrades;
        public decimal distance;
        private string trade;
        private string logFile;
        private int maxTradesPerDay;
        private string currencyPair;
        public string timeFrame;
        private decimal riskPercent;
        private decimal riskVsReward;
        private decimal startingBalance;
        private DateTime startDate;
        private string connectionString;
        private decimal accountBalance;

        private void Profit()
        {
            accountBalance = accountBalance + (accountBalance * riskPercent*riskVsReward);
        }

        private void Loss()
        {
            accountBalance = accountBalance - (accountBalance * riskPercent);
        }

        private void WriteLog(int i)
        
        {
            //build log

            tradeEngineLog = new LogData
            {
                SystemTime = DateTime.Now,
                BarTime = bars.candles[i].time,
                BarOpen = bars.candles[i].mid.o,
                BarClose = bars.candles[i].mid.c,
                BarHigh = bars.candles[i].mid.h,
                BarLow = bars.candles[i].mid.l,
                Volume = bars.candles[i].volume,
                SMA50 = indicator[i].result,
                Trend = trend,
                StandardDeviation = standardDeviation,
                Distance = distance,
                TakeProfit = takeProfit,
                StopLoss = stopLoss,
                Trade = trade,
                ProfitTrades = profitTrades,
                LossTrades=lossTrades,
                TotalTrades=totalTrades
                
            };

            //write to csv file

            using (StreamWriter writer = new StreamWriter(logFile, append: true))
            {
                CsvWriter csvWriter = new CsvWriter(writer);
                csvWriter.WriteRecord(tradeEngineLog);
                csvWriter.NextRecord();

            }
        }


        public BackTest(BackTestConfiguration _config)
        {
            //set internal vars

            runLength = _config.runLength;
            maxTradesPerDay = _config.maxTradesPerDay;
            currencyPair = _config.currencyPair;
            timeFrame = _config.timeFrame;
            riskPercent = _config.riskPercent;
            riskVsReward = _config.riskVsReward;
            startingBalance = _config.startingBalance;
            startDate = _config.startDate;
            connectionString = _config.connectionString;
            //initialize defaults

            profitTrades = default;
            lossTrades = default;
            totalTrades = default;
            trade = default;
            
            closingPrice = new List<double>();
            myAccountSettings = new AccountSettings { BASEURI = BASEURI, APIKEY = APIKEY, ID = ID };

            myAccount = new AccountAccessor(myAccountSettings);
            instruments = new InstrumentAccessor(myAccountSettings, currencyPair);


            orders = new OrderAccessor(myAccountSettings);
           
            

        }

        public void StoreData(string[] _timeFrame)
        {
            //shouldn't need to do this
            SqlConnection dbConnect = new SqlConnection(connectionString);
            dbConnect.Open();
            EUR_USDDataContext linqInterop = new EUR_USDDataContext(dbConnect);
           
                        

            ProgressBarOptions progressBarOptions = new ProgressBarOptions { ProgressCharacter = '*' };
            ProgressBar progressBar = new ProgressBar(runLength*_timeFrame.Count() - 1, "Saving bar data to local storage", progressBarOptions);

            foreach (var tf in _timeFrame)
            {

                for (int i = 0; i < runLength - 1; i++)


                {
                    DateTime runDate = startDate.AddDays(i);

                    //can't trade on weekends and december 25th, Jan 1st
                    if (!(runDate.DayOfWeek == DayOfWeek.Saturday) && !(runDate.DayOfWeek == DayOfWeek.Sunday) && !(runDate.Date == new DateTime(runDate.Year, 12, 25)) && !(runDate.Date == new DateTime(runDate.Year, 1, 1)))
                    {
                        DateTime startTime = runDate.Add(new TimeSpan(00, 00, 00));
                        DateTime endTime = runDate.Add(new TimeSpan(23, 59, 59));
                        bars = instruments.GetBars(startTime, endTime, tf);
                        for (int i2 = 0; i2 < bars.candles.Count - 1; i2++)
                        {
                            //write rows to db

                            BarData row = new BarData
                            {
                                SystemTime = DateTime.Now,
                                BarTime = bars.candles[i2].time,
                                c = bars.candles[i2].mid.c,
                                o = bars.candles[i2].mid.o,
                                h = bars.candles[i2].mid.h,
                                l = bars.candles[i2].mid.l,
                                Volume = bars.candles[i2].volume,
                                Timeframe=tf
                            };
                            linqInterop.BarDatas.InsertOnSubmit(row);



                        }

                        linqInterop.SubmitChanges();


                    }

                    progressBar.Tick();
                }
            }

            //clean up
            linqInterop.Dispose();
            dbConnect.Close();

        }

    
        public void Reset (string[] _tables)
            //delete all rows in specified tables, tables names should be from DataContext NOT SQL server table names
        {
            SqlConnection dbConnect = new SqlConnection(connectionString);
            dbConnect.Open();
            EUR_USDDataContext linqInterop = new EUR_USDDataContext(dbConnect);
            foreach (var i in _tables)
            {
                var table = (ITable)linqInterop.GetType().GetProperty(i).GetValue(linqInterop, null);
                table.DeleteAllOnSubmit(table);
                linqInterop.SubmitChanges();
            }
            linqInterop.Dispose();
            dbConnect.Close();


        }

        public void Run(string _algorithim, string[] _timeFrames)
        {
            //run backtesting against specific algorithim against multiple timeframes

            SqlConnection dbConnect = new SqlConnection(connectionString);
            dbConnect.Open();
            EUR_USDDataContext linqInterop = new EUR_USDDataContext(dbConnect);

            ProgressBarOptions progressBarOptions = new ProgressBarOptions { ProgressCharacter = '-' };
            ProgressBar progressBar = new ProgressBar(runLength*_timeFrames.Count() , "Backtesting against Bar data", progressBarOptions);

            foreach (var tf in _timeFrames)
            {
                timeFrame = tf;
                totalTrades = default;
                profitTrades = default;
                lossTrades = default;
                accountBalance = startingBalance;

                for (int i = 0; i < runLength - 1; i++)
                
                {
                    


                    bars = new OandaRootInstrumentObject {candles=new List<Candle>(),granularity=tf, instrument="EUR_USD"};
                    DateTime runDate = startDate.AddDays(i);

                    //won't be days in DB for weekends, or december 25th, jan 01

                    if (!(runDate.DayOfWeek == DayOfWeek.Saturday) && !(runDate.DayOfWeek == DayOfWeek.Sunday) && !(runDate.Date == new DateTime(runDate.Year, 12, 25)) && !(runDate.Date == new DateTime(runDate.Year, 1, 1)))
                    {
                        //get all bars for the day for the selected timeframe, ordered by ID ascending, ID is identity and PK so these should always be unique in ascending order

                        
                        var results = from r in linqInterop.BarDatas where (r.BarTime.Date == runDate.Date && r.Timeframe == tf) orderby r.ID ascending select r ;
                        //need to massage data from sql db to OandaRootInstrumentObject
                        
                        foreach (var bar in results)
                        {
                            bars.candles.Add(new Candle { volume = bar.Volume, mid = (new Mid { c = bar.c, h = bar.h, l = bar.l, o = bar.o }), time = bar.BarTime });
                        }

                        indicator = new List<IndicatorOverTime>();
                        indicator.Add(new IndicatorOverTime { result = bars.candles[0].mid.c, time = bars.candles[0].time });


                        trend = new TrendDirection();

                        //set because log file is written before trend is actually calculated, so should be sideways until there is enough data to actually determine the trend
                        trend = TrendDirection.Sideways;


                        switch (_algorithim)
                        {
                            case "SimpleTrendFollowing":
                                {
                                    SimpleTrendFollowing();
                                    break;
                                }



                        }
                    }
                    progressBar.Tick();

                }
                        

                            
                

            }

            linqInterop.Dispose();
            dbConnect.Close();

        }
        private void SimpleTrendFollowing()
        {
            
            
            SqlConnection dbConnect = new SqlConnection(connectionString);
            dbConnect.Open();
            EUR_USDDataContext linqInterop = new EUR_USDDataContext(dbConnect);

           void BuildLog(int _i)
            //build object for logging to database

            {
                SimpleTrendFollowing row = new SimpleTrendFollowing
                {
                    BarTime = bars.candles[_i].time,
                    SystemTime = DateTime.Now,
                    StandardDeviation = standardDeviation,
                    TimeFrame = timeFrame,
                    SMA50 = indicator[_i].result,
                    LossTrades = lossTrades,
                    ProfitTrades = profitTrades,
                    TakeProfit = takeProfit,
                    Distance = distance,
                    TotalTrades = totalTrades,
                    Trade = trade,
                    StopLoss=stopLoss,
                    AccountBalance=accountBalance,
                    Trend = trend.ToString()
                    

                };
                linqInterop.SimpleTrendFollowings.InsertOnSubmit(row);
                linqInterop.SubmitChanges();


            }

            decimal padding = 1.0M; //added to standard deviation for stoploss calculation
            int tradesPerDay = maxTradesPerDay;
            bool openOrder = false;
            int windowSize = 50;    //should be same as SMA period
            

            //trade when market is strongest, EST
            TimeSpan start = new TimeSpan(05, 00, 00);
            TimeSpan end = new TimeSpan(22, 59, 00);

            for (int i = 1; i <= bars.candles.Count - 1; i++)
            {
                trade = default;

                //create sliding window 


                OandaRootInstrumentObject barWindow = new OandaRootInstrumentObject();
                if (i < windowSize)
                {
                    barWindow.candles = new List<Candle>();

                    barWindow.candles = bars.candles.GetRange(0, i + 1);


                }
                /*
                else if (i>bars.candles.Count-1-windowSize)
                {
                    barWindow.candles = new List<Candle>();
                    barWindow.candles = bars.candles.GetRange(i, bars.candles.Count-i);
                }
                */

                else
                {
                    barWindow.candles = new List<Candle>();
                    barWindow.candles = bars.candles.GetRange(i - windowSize, windowSize);
                }

                indicator.Add(new IndicatorOverTime { result = Indicators.SMA(barWindow), time = bars.candles[i].time });
                trend = Indicators.Trend(indicator);


                //if time of bar within trading window
                if (bars.candles[i].time.TimeOfDay <= end && bars.candles[i].time.TimeOfDay >= start && openOrder == false)
                {
                    if (tradesPerDay > 0)
                    {
                        if (trend == TrendDirection.StrongUp) //go long
                        {
                            trade = "Long:Start";
                            closingPrice = new List<double>();
                            //get closing price for last 100 bars to calc standard deviation
                            for (int i2 = 0; i2 < barWindow.candles.Count - 1; i2++)
                            {
                                closingPrice.Add((double)barWindow.candles[i2].mid.c);
                            }
                            standardDeviation = (decimal)MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(closingPrice.ToArray());
                            distance = (standardDeviation * 2 + (standardDeviation * padding));
                            stopLoss = (decimal)bars.candles[i].mid.c - distance;
                            takeProfit = (decimal)bars.candles[i].mid.c + (distance * riskVsReward);
                            tradesPerDay = tradesPerDay - 1;
                            openOrder = true;
                            BuildLog(i);
                        }
                        else if (trend == TrendDirection.StrongDown) //short
                        {
                            trade = "Short:Start";
                            closingPrice = new List<double>();
                            //get closing price for last 100 bars to calc standard deviation
                            for (int i2 = 0; i2 < barWindow.candles.Count - 1; i2++)
                            {
                                closingPrice.Add((double)barWindow.candles[i2].mid.c);
                            }
                            standardDeviation = (decimal)MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(closingPrice.ToArray());
                            distance = (standardDeviation * 2 + (standardDeviation * padding));
                            stopLoss = (decimal)bars.candles[i].mid.c + distance;
                            takeProfit = (decimal)bars.candles[i].mid.c - (distance * riskVsReward);
                            tradesPerDay = tradesPerDay - 1;
                            openOrder = true;
                            BuildLog(i);
                        }


                    }
                }
                else if (openOrder == true) //check if price targets have been met
                {
                    if (stopLoss > takeProfit) //short
                    {
                        if (bars.candles[i].mid.c <= takeProfit)  //profit
                        {
                            openOrder = false;
                            trade = "Short:Profit";
                            profitTrades++;
                            totalTrades = profitTrades + lossTrades;
                            Profit();
                            BuildLog(i);

                        }
                        else if (bars.candles[i].mid.c >= stopLoss)  //loss
                        {
                            openOrder = false;
                            trade = "Short:Loss";
                            lossTrades++;
                            totalTrades = profitTrades + lossTrades;
                            Loss();
                            BuildLog(i);

                        }


                    }
                    else  //long
                    {
                        if (bars.candles[i].mid.c >= takeProfit) //profit
                        {
                            openOrder = false;
                            trade = "Long:Profit";
                            profitTrades++;
                            totalTrades = profitTrades + lossTrades;
                            Profit();
                            BuildLog(i);


                        }
                        else if (bars.candles[i].mid.c <= stopLoss) //loss
                        {
                            openOrder = false;
                            trade = "Long:Loss";
                            lossTrades++;
                            totalTrades = profitTrades + lossTrades;
                            Loss();
                            BuildLog(i);

                        }



                    }


                }

                if (bars.candles[i].time.TimeOfDay == new TimeSpan(00, 00, 00)) //check for end of day, reset count
                {
                    tradesPerDay = maxTradesPerDay;
                    //closingPrice = new List<double>();
                    //closedTrade = default;

                }
                


                


            }

            linqInterop.Dispose();
            dbConnect.Close();
            
        }


    }
}

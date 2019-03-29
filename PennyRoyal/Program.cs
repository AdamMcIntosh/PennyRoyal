using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
namespace PennyRoyal
{
    public enum TrendDirection { StrongUp, StrongDown,WeakUp, WeakDown, Sideways }

    class Program
    {
        static void Main(string[] args)


        {
            Console.Clear();

            DateTime startTime = DateTime.Today;
            //TradeEngine genesis = new TradeEngine();
            BackTest();

            do
            {

                //yep, looping, should anything go here?
            } while (DateTime.Today < startTime.AddDays(5)); //run for 5 days



        }

        private static void BackTest()
        {
            BackTestConfiguration backTestConfiguration = new BackTestConfiguration
            {
                logFile = "BackTestLog",
                currencyPair = "EUR_USD",
                timeFrame = "M1",
                riskPercent = .025M,
                riskVsReward=1.25M,
                maxTradesPerDay=2,
                startingBalance=1000.00M,
                startDate=new DateTime(2010,1,1),
                runLength=2995,
                connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\pennyroyaldb\EUR_USD.mdf;Integrated Security=True"
            };

            BackTest backTest = new BackTest(backTestConfiguration);
            //backTest.StoreData(new string[] { "M1","M2","M4","M5","M10","M15"});
            backTest.Reset(new string[] { "SimpleTrendFollowings" });
            backTest.Run("SimpleTrendFollowing", new string[] { "M1","M2","M4","M5","M10" });
            Environment.Exit(0);
        }

    }
}

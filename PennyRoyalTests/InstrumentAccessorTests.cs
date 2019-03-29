using Microsoft.VisualStudio.TestTools.UnitTesting;
using PennyRoyal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal.Tests
{
    [TestClass()]
    public class InstrumentAccessorTests
    {
        public const string BASEURI = "https://api-fxpractice.oanda.com/v3";
        public const string APIKEY = "efbbb5534290d411a67cc004d7411042-34ca9dde94de7cc08dbc96184702d0ab";
        public const string ID = "101-001-7625641-001";



        [TestMethod()]
        public void InstrumentAccessorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OnUpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBarsPeriodTest()
        {
            var myAccountSettings = new AccountSettings { BASEURI = BASEURI, APIKEY = APIKEY, ID = ID };
            var instrument = new InstrumentAccessor(myAccountSettings,"EUR_USD");
            int period = 25;
            var bars = instrument.GetBars(period, "M5");

            Assert.AreEqual(period, bars.candles.Count);
        }
        [TestMethod()]
        public void GetBarsTimeFrameTest()
        {
            var myAccountSettings = new AccountSettings { BASEURI = BASEURI, APIKEY = APIKEY, ID = ID };
            var instrument = new InstrumentAccessor(myAccountSettings, "EUR_USD");
                    
            int period = 25;
            string timeFrame = "M15";
            var bars = instrument.GetBars(period, timeFrame);

            Assert.AreEqual(timeFrame,bars.granularity);
        }


    }
}
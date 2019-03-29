using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyRoyal
{
    static class Indicators
    {
        static public decimal SMA(OandaRootInstrumentObject _bars)
        //simple moving average
        {
            decimal sum = 0.0M;
            foreach (var i in _bars.candles)
            {
               sum = i.mid.c + sum;
            }
            return sum / _bars.candles.Count();

        }


        static public decimal EMA(OandaRootInstrumentObject _bars)
        //exponential moving average
        {
            return 0.0M;
        }

        static public TrendDirection Trend(List<IndicatorOverTime> _indicatorOverTime)
        //direction of the trend
        {
            int upSum = default;
            int downSum = default;
            decimal SMA = default;
            int period = 10;
            decimal upPercent = default;
            //check for increasing moving average

            if (_indicatorOverTime.Count<=period)
            {
                //if there aren't enough samples we can't determine the trend, so assume sideways
                return TrendDirection.Sideways;
            }
            else
            {
                //trend based upon last <period> bars

                for (int i = _indicatorOverTime.Count-period+1; i <= _indicatorOverTime.Count-1; i++)
                {
                    SMA = _indicatorOverTime[i].result;
                    if (SMA > _indicatorOverTime[i - 1].result)
                    {
                        upSum++;
                    }
                    else
                    {
                        downSum++;
                    }

                }

                try
                {
                    upPercent = (decimal)upSum / (decimal)downSum;
                }
                catch
                {
                    return TrendDirection.StrongUp;
                }
                
                if (upPercent >2.0M)
                {
                    return TrendDirection.StrongUp;
                }
                else if (upPercent >1.0M)
                {
                    return TrendDirection.WeakUp;
                }
                else if (upPercent == 0.0M)
                {
                    return TrendDirection.StrongDown;
                }
                else if (upPercent <0.5M)
                {
                    return TrendDirection.StrongDown;
                }
                else if (upPercent<0.8M)
                {
                    return TrendDirection.WeakDown;
                }

            }


            return TrendDirection.Sideways;
        }

    }
}

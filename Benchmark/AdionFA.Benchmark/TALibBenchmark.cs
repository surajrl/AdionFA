using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Data;

namespace AdionFA.Benchmark
{
    [MemoryDiagnoser]
    public class TALibBenchmark
    {
        private static readonly int _numOfItems = 3000;

        private static double[] _open = new double[_numOfItems];
        private static double[] _high = new double[_numOfItems];
        private static double[] _low = new double[_numOfItems];
        private static double[] _close = new double[_numOfItems];

        //public static void Main(string[] args)
        //{
        //    _open = GenerateOHLC();
        //    _high = GenerateOHLC();
        //    _low = GenerateOHLC();
        //    _close = GenerateOHLC();

        //    BenchmarkRunner.Run<TALibBenchmark>();
        //}

        [Benchmark]
        public void STOCH_1_Performance()
        {
            var indicator = new STOCH
            {
                Type = IndicatorEnum.STOCH,

                OptInFastKPeriod = 7,
                OptInSlowKPeriod = 5,
                OptInSlowKMAType = 5,
                OptInSlowDPeriod = 13,
                OptInSlowDMAType = 7,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var STOCH = indicator;
                TALib.Core.Stoch(_high, _low, _close, startIdx, endIdx, STOCH.Output, new double[length], out var OutBegIdx, out var OutNBElement, (TALib.Core.MAType)STOCH.OptInSlowKMAType, (TALib.Core.MAType)STOCH.OptInSlowDMAType, STOCH.OptInFastKPeriod, STOCH.OptInSlowKPeriod, STOCH.OptInSlowDPeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void STOCH_2_Performance()
        {
            var indicator = new STOCH
            {
                Type = IndicatorEnum.STOCH,

                OptInFastKPeriod = 17,
                OptInSlowKPeriod = 3,
                OptInSlowKMAType = 1,
                OptInSlowDPeriod = 14,
                OptInSlowDMAType = 5,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var STOCH = indicator;
                TALib.Core.Stoch(_high, _low, _close, startIdx, endIdx, STOCH.Output, new double[length], out var OutBegIdx, out var OutNBElement, (TALib.Core.MAType)STOCH.OptInSlowKMAType, (TALib.Core.MAType)STOCH.OptInSlowDMAType, STOCH.OptInFastKPeriod, STOCH.OptInSlowKPeriod, STOCH.OptInSlowDPeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void RSI_1_Performance()
        {
            var indicator = new RSI
            {
                Type = IndicatorEnum.RSI,

                PriceType = 3,

                OptInTimePeriod = 7,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var RSI = indicator;
                TALib.Core.Rsi(BuildInRealArray(RSI.PriceType, _open, _high, _low, _close), startIdx, endIdx, RSI.Output, out var OutBegIdx, out var OutNBElement, RSI.OptInTimePeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void STOCHRSI_1_Performance()
        {
            var indicator = new STOCHRSI
            {
                Type = IndicatorEnum.STOCHRSI,

                PriceType = 3,

                OptInFastDMAType = 1,
                OptInFastDPeriod = 12,
                OptInFastKPeriod = 9,
                OptInTimePeriod = 27,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                var OutBegIdx = 0;
                var OutNBElement = 0;

                // Run Indicator ...
                var STOCHRSI = indicator;
                switch (STOCHRSI.STOCHRSIOutput)
                {
                    case (int)STOCHRSIOutputEnum.FastK:
                        TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, _open, _high, _low, _close), startIdx, endIdx, STOCHRSI.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                        break;

                    case (int)STOCHRSIOutputEnum.FastD:
                        TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, _open, _high, _low, _close), startIdx, endIdx, new double[length], STOCHRSI.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                        break;
                }

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void STOCHRSI_2_Performance()
        {
            var indicator = new STOCHRSI
            {
                Type = IndicatorEnum.STOCHRSI,

                PriceType = 3,

                OptInFastDMAType = 6,
                OptInFastDPeriod = 20,
                OptInFastKPeriod = 18,
                OptInTimePeriod = 52,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                var OutBegIdx = 0;
                var OutNBElement = 0;

                // Run Indicator ...
                var STOCHRSI = indicator;
                switch (STOCHRSI.STOCHRSIOutput)
                {
                    case (int)STOCHRSIOutputEnum.FastK:
                        TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, _open, _high, _low, _close), startIdx, endIdx, STOCHRSI.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                        break;

                    case (int)STOCHRSIOutputEnum.FastD:
                        TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, _open, _high, _low, _close), startIdx, endIdx, new double[length], STOCHRSI.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                        break;
                }

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void MOM_1_Performance()
        {
            var indicator = new MOM
            {
                Type = IndicatorEnum.MOM,

                PriceType = 3,

                OptInTimePeriod = 5,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var MOM = indicator;
                TALib.Core.Mom(BuildInRealArray(MOM.PriceType, _open, _high, _low, _close), startIdx, endIdx, MOM.Output, out var OutBegIdx, out var OutNBElement, MOM.OptInTimePeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void WILLR_1_Performance()
        {
            var indicator = new WILLR
            {
                Type = IndicatorEnum.WILLR,

                OptInTimePeriod = 32,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var WILLR = indicator;
                TALib.Core.WillR(_high, _low, _close, startIdx, endIdx, WILLR.Output, out var OutBegIdx, out var OutNBElement, WILLR.OptInTimePeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void WILLR_2_Performance()
        {
            var indicator = new WILLR
            {
                Type = IndicatorEnum.WILLR,

                OptInTimePeriod = 58,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var WILLR = indicator;
                TALib.Core.WillR(_high, _low, _close, startIdx, endIdx, WILLR.Output, out var OutBegIdx, out var OutNBElement, WILLR.OptInTimePeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void AROON_1_Performance()
        {
            var indicator = new AROON
            {
                Type = IndicatorEnum.AROON,

                OptInTimePeriod = 12,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var AROON = indicator;

                var up = new double[length];
                var down = new double[length];

                var OutBegIdx = 0;
                var OutNBElement = 0;

                switch (AROON.AROONDownUp)
                {
                    case (int)AROONDownUpEnum.UP:
                        TALib.Core.Aroon(_high, _low, startIdx, endIdx, down, AROON.Output, out OutBegIdx, out OutNBElement, AROON.OptInTimePeriod);
                        break;

                    case (int)AROONDownUpEnum.DOWN:
                        TALib.Core.Aroon(_high, _low, startIdx, endIdx, AROON.Output, up, out OutBegIdx, out OutNBElement, AROON.OptInTimePeriod);
                        break;
                }

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        [Benchmark]
        public void PLUSDI_1_Performance()
        {
            var indicator = new PLUS_DI
            {
                Type = IndicatorEnum.PLUS_DI,

                OptInTimePeriod = 12,
            };

            for (var i = 0; i < _numOfItems; i++)
            {
                var startIdx = 0;
                var endIdx = i;

                var length = endIdx - startIdx + 1;
                indicator.Output = new double[length];

                // Run Indicator ...
                var PLUS_DI = indicator;
                TALib.Core.PlusDI(_high, _low, _close, startIdx, endIdx, PLUS_DI.Output, out var OutBegIdx, out var OutNBElement, PLUS_DI.OptInTimePeriod);

                indicator.OutBegIdx = OutBegIdx;
                indicator.OutNBElement = OutNBElement;
            }
        }

        public double[] BuildInRealArray(int priceType, double[] open, double[] high, double[] low, double[] close)
        {
            return priceType switch
            {
                (int)PriceTypeEnum.LOW => low,
                (int)PriceTypeEnum.CLOSE => close,
                (int)PriceTypeEnum.OPEN => open,
                _ => high
            };
        }

        public static double[] GenerateOHLC()
        {
            var rand = new Random();

            return Enumerable.Range(0, _numOfItems)
                .Select(i => new Tuple<int, int>(rand.Next(100), i))
                .OrderBy(i => i.Item1)
                .Select(i => (double)i.Item2)
                .ToArray();
        }
    }
}
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Enums;
using System.Diagnostics;

namespace AdionFA.Benchmark
{
    public class Extractor
    {
        public static IList<IndicatorBase> BuildIndicatorsFromNode(IList<string> node)
        {
            try
            {
                var indicators = new List<IndicatorBase>();
                foreach (var n in node)
                {
                    var f = n.Replace("|", string.Empty).Replace(" ", string.Empty);

                    MathOperatorEnum? optor = null;
                    string[]? divisions = null;

                    // Operator Split

                    if (f.Contains(">="))
                    {
                        optor = MathOperatorEnum.GreaterThanOrEqual;
                        divisions = f.Split(">=");
                    }
                    else if (f.Contains("<="))
                    {
                        optor = MathOperatorEnum.LessThanOrEqual;
                        divisions = f.Split("<=");
                    }
                    else if (f.Contains('>'))
                    {
                        optor = MathOperatorEnum.GreaterThan;
                        divisions = f.Split('>');
                    }
                    else if (f.Contains('<'))
                    {
                        optor = MathOperatorEnum.LessThan;
                        divisions = f.Split('<');
                    }
                    else if (f.Contains('='))
                    {
                        optor = MathOperatorEnum.Equal;
                        divisions = f.Split('=');
                    }

                    // Params Split
                    if (divisions?.Length == 2)
                    {
                        var indicatorParams = divisions[0].Split("_");

                        var indicatorName = indicatorParams[0].Replace(".", "_");

                        if (Enum.TryParse(indicatorName, out IndicatorEnum indicatorType))
                        {
                            switch (indicatorType)
                            {
                                case IndicatorEnum.ADX:
                                    indicators.Add(new ADX
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[1])
                                    });
                                    break;

                                case IndicatorEnum.ADXR:
                                    indicators.Add(new ADXR
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[1])
                                    });
                                    break;

                                case IndicatorEnum.APO:
                                    indicators.Add(new APO
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInFastPeriod = int.Parse(indicatorParams[2]),
                                        OptInSlowPeriod = int.Parse(indicatorParams[3]),
                                        MAType = int.Parse(indicatorParams[4])
                                    });
                                    break;

                                case IndicatorEnum.AROON:
                                    indicators.Add(new AROON
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                        AROONDownUp = int.Parse(indicatorParams[2])
                                    });
                                    break;

                                case IndicatorEnum.ATR:
                                    indicators.Add(new ATR
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.BOP:
                                    indicators.Add(new BOP());
                                    break;

                                case IndicatorEnum.CCI:
                                    indicators.Add(new CCI
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.CMO:
                                    indicators.Add(new CMO
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                    });
                                    break;

                                case IndicatorEnum.DX:
                                    indicators.Add(new DX
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.MACD:
                                    indicators.Add(new MACD
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInFastPeriod = int.Parse(indicatorParams[2]),
                                        OptInSlowPeriod = int.Parse(indicatorParams[3]),
                                        OptInSignalPeriod = int.Parse(indicatorParams[4]),
                                        MACDOutput = int.Parse(indicatorParams[5]),
                                    });
                                    break;

                                case IndicatorEnum.MINUS_DI:
                                    indicators.Add(new MINUS_DI
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.MINUS_DM:
                                    indicators.Add(new MINUS_DM
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.MOM:
                                    indicators.Add(new MOM
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                    });
                                    break;

                                case IndicatorEnum.PLUS_DI:
                                    indicators.Add(new PLUS_DI
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.PLUS_DM:
                                    indicators.Add(new PLUS_DM
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1]),
                                    });
                                    break;

                                case IndicatorEnum.PPO:
                                    indicators.Add(new PPO
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInFastPeriod = int.Parse(indicatorParams[2]),
                                        OptInSlowPeriod = int.Parse(indicatorParams[3]),
                                        MAType = int.Parse(indicatorParams[4]),
                                    });
                                    break;

                                case IndicatorEnum.ROC:
                                    indicators.Add(new ROC
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                    });
                                    break;

                                case IndicatorEnum.RSI:
                                    indicators.Add(new RSI
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                    });
                                    break;

                                case IndicatorEnum.STDDEV:
                                    indicators.Add(new STDDEV
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                        OptInNbDev = double.Parse(indicatorParams[3]),
                                    });
                                    break;

                                case IndicatorEnum.STOCHF:
                                    indicators.Add(new STOCHF
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInFastKPeriod = int.Parse(indicatorParams[1]),
                                        OptInFastDPeriod = int.Parse(indicatorParams[2]),
                                        OptInFastDMAType = int.Parse(indicatorParams[3]),
                                        STOCHFOutput = int.Parse(indicatorParams[4]),
                                    });
                                    break;

                                case IndicatorEnum.STOCH:
                                    indicators.Add(new STOCH
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInFastKPeriod = int.Parse(indicatorParams[1]),
                                        OptInSlowKPeriod = int.Parse(indicatorParams[2]),
                                        OptInSlowKMAType = int.Parse(indicatorParams[3]),
                                        OptInSlowDPeriod = int.Parse(indicatorParams[4]),
                                        OptInSlowDMAType = int.Parse(indicatorParams[5]),
                                    });
                                    break;

                                case IndicatorEnum.STOCHRSI:
                                    indicators.Add(new STOCHRSI
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                        OptInFastKPeriod = int.Parse(indicatorParams[3]),
                                        OptInFastDPeriod = int.Parse(indicatorParams[4]),
                                        OptInFastDMAType = int.Parse(indicatorParams[5]),
                                        STOCHRSIOutput = int.Parse(indicatorParams[6]),
                                    });
                                    break;

                                case IndicatorEnum.ULTOSC:
                                    indicators.Add(new ULTOSC
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod1 = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod2 = int.Parse(indicatorParams[2]),
                                        OptInTimePeriod3 = int.Parse(indicatorParams[3]),
                                    });
                                    break;

                                case IndicatorEnum.VAR:
                                    indicators.Add(new VAR
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        PriceType = int.Parse(indicatorParams[1]),
                                        OptInTimePeriod = int.Parse(indicatorParams[2]),
                                    });
                                    break;

                                case IndicatorEnum.WILLR:
                                    indicators.Add(new WILLR
                                    {
                                        Operator = optor,
                                        Value = double.Parse(divisions[1]),

                                        OptInTimePeriod = int.Parse(indicatorParams[1])
                                    });
                                    break;
                            }
                        }
                    }
                }

                return indicators;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public static IList<IndicatorBase> CalculateNodeIndicators(
            Candle firstCandle,
            Candle currentCandle,
            IList<IndicatorBase> indicators,
            IEnumerable<Candle> candleHistory)
        {
            try
            {
                var extractions = new List<IndicatorBase>(indicators);

                var open = (from c in candleHistory select c.Open).ToArray();
                var high = (from c in candleHistory select c.High).ToArray();
                var low = (from c in candleHistory select c.Low).ToArray();
                var close = (from c in candleHistory select c.Close).ToArray();

                var startIdx = Array.IndexOf(candleHistory.ToArray(), firstCandle);
                var endIdx = Array.IndexOf(candleHistory.ToArray(), currentCandle);

                Execute(startIdx, endIdx, open, high, low, close, extractions, candleHistory, 0, false);

                return extractions;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private static void Execute(
            int startIdx,
            int endIdx,
            double[] open,
            double[] high,
            double[] low,
            double[] close,
            IList<IndicatorBase> indicators,
            IEnumerable<Candle> candlesRange,
            int timeframeId,
            bool isExtraction)
        {
            try
            {
                var length = endIdx - startIdx + 1;
                foreach (var indicator in indicators)
                {
                    indicator.Output = new double[length];

                    var OutNBElement = 0;
                    var OutBegIdx = 0;

                    switch (indicator.Type)
                    {
                        case IndicatorEnum.ADX:
                            var ADX = (ADX)indicator;
                            TALib.Core.Adx(high, low, close, startIdx, endIdx, ADX.Output, out OutBegIdx, out OutNBElement, ADX.OptInTimePeriod);
                            break;

                        case IndicatorEnum.ADXR:
                            var ADXR = (ADXR)indicator;
                            TALib.Core.Adxr(high, low, close, startIdx, endIdx, ADXR.Output, out OutBegIdx, out OutNBElement, ADXR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.APO:
                            var APO = (APO)indicator;
                            TALib.Core.Apo(BuildInRealArray(APO.PriceType, open, high, low, close), startIdx, endIdx, APO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)APO.MAType, APO.OptInFastPeriod, APO.OptInSlowPeriod);
                            break;

                        case IndicatorEnum.AROON:
                            var AROON = (AROON)indicator;

                            var up = new double[length];
                            var down = new double[length];

                            switch (AROON.AROONDownUp)
                            {
                                case (int)AROONDownUpEnum.UP:
                                    TALib.Core.Aroon(high, low, startIdx, endIdx, down, AROON.Output, out OutBegIdx, out OutNBElement, AROON.OptInTimePeriod);
                                    break;

                                case (int)AROONDownUpEnum.DOWN:
                                    TALib.Core.Aroon(high, low, startIdx, endIdx, AROON.Output, up, out OutBegIdx, out OutNBElement, AROON.OptInTimePeriod);
                                    break;
                            }
                            break;

                        case IndicatorEnum.ATR:
                            var ATR = (ATR)indicator;
                            TALib.Core.Atr(high, low, close, startIdx, endIdx, ATR.Output, out OutBegIdx, out OutNBElement, ATR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.BOP:
                            var BOP = (BOP)indicator;
                            TALib.Core.Bop(open, high, low, close, startIdx, endIdx, BOP.Output, out OutBegIdx, out OutNBElement);
                            break;

                        case IndicatorEnum.CCI:
                            var CCI = (CCI)indicator;
                            TALib.Core.Cci(high, low, close, startIdx, endIdx, CCI.Output, out OutBegIdx, out OutNBElement, CCI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.CMO:
                            var CMO = (CMO)indicator;
                            TALib.Core.Cmo(BuildInRealArray(CMO.PriceType, open, high, low, close), startIdx, endIdx, CMO.Output, out OutBegIdx, out OutNBElement, CMO.OptInTimePeriod);
                            break;

                        case IndicatorEnum.DX:
                            var DX = (DX)indicator;
                            TALib.Core.Dx(high, low, close, startIdx, endIdx, DX.Output, out OutBegIdx, out OutNBElement, DX.OptInTimePeriod);
                            break;

                        case IndicatorEnum.MACD:
                            var MACD = (MACD)indicator;

                            var macd = new double[length];
                            var signal = new double[length];
                            var hist = new double[length];

                            switch (MACD.MACDOutput)
                            {
                                case (int)MACDOutputEnum.Macd:
                                    TALib.Core.Macd(BuildInRealArray(MACD.PriceType, open, high, low, close), startIdx, endIdx, MACD.Output, signal, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;

                                case (int)MACDOutputEnum.MacdSignal:
                                    TALib.Core.Macd(BuildInRealArray(MACD.PriceType, open, high, low, close), startIdx, endIdx, macd, MACD.Output, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;

                                case (int)MACDOutputEnum.MacdHist:
                                    TALib.Core.Macd(BuildInRealArray(MACD.PriceType, open, high, low, close), startIdx, endIdx, macd, signal, MACD.Output, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;
                            }
                            break;

                        case IndicatorEnum.MINUS_DI:
                            var MINUS_DI = (MINUS_DI)indicator;
                            TALib.Core.MinusDI(high, low, close, startIdx, endIdx, MINUS_DI.Output, out OutBegIdx, out OutNBElement, MINUS_DI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.MINUS_DM:
                            var MINUS_DM = (MINUS_DM)indicator;
                            TALib.Core.MinusDM(high, low, startIdx, endIdx, MINUS_DM.Output, out OutBegIdx, out OutNBElement, MINUS_DM.OptInTimePeriod);
                            break;

                        case IndicatorEnum.MOM:
                            var MOM = (MOM)indicator;
                            TALib.Core.Mom(BuildInRealArray(MOM.PriceType, open, high, low, close), startIdx, endIdx, MOM.Output, out OutBegIdx, out OutNBElement, MOM.OptInTimePeriod);
                            break;

                        case IndicatorEnum.PLUS_DI:
                            var PLUS_DI = (PLUS_DI)indicator;
                            TALib.Core.PlusDI(high, low, close, startIdx, endIdx, PLUS_DI.Output, out OutBegIdx, out OutNBElement, PLUS_DI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.PLUS_DM:
                            var PLUS_DM = (PLUS_DM)indicator;
                            TALib.Core.PlusDM(high, low, startIdx, endIdx, PLUS_DM.Output, out OutBegIdx, out OutNBElement, PLUS_DM.OptInTimePeriod);
                            break;

                        case IndicatorEnum.PPO:
                            var PPO = (PPO)indicator;
                            TALib.Core.Ppo(BuildInRealArray(PPO.PriceType, open, high, low, close), startIdx, endIdx, PPO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)PPO.MAType, PPO.OptInFastPeriod, PPO.OptInSlowPeriod);
                            break;

                        case IndicatorEnum.ROC:
                            var ROC = (ROC)indicator;
                            TALib.Core.Roc(BuildInRealArray(ROC.PriceType, open, high, low, close), startIdx, endIdx, ROC.Output, out OutBegIdx, out OutNBElement, ROC.OptInTimePeriod);
                            break;

                        case IndicatorEnum.RSI:
                            var RSI = (RSI)indicator;
                            TALib.Core.Rsi(BuildInRealArray(RSI.PriceType, open, high, low, close), startIdx, endIdx, RSI.Output, out OutBegIdx, out OutNBElement, RSI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.STDDEV:
                            var STDDEV = (STDDEV)indicator;
                            TALib.Core.StdDev(BuildInRealArray(STDDEV.PriceType, open, high, low, close), startIdx, endIdx, STDDEV.Output, out OutBegIdx, out OutNBElement, STDDEV.OptInTimePeriod, STDDEV.OptInNbDev);
                            break;

                        case IndicatorEnum.STOCHF:
                            var STOCHF = (STOCHF)indicator;
                            switch (STOCHF.STOCHFOutput)
                            {
                                case (int)STOCHFOutputEnum.FastK:
                                    TALib.Core.StochF(high, low, close, startIdx, endIdx, STOCHF.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHF.OptInFastDMAType, STOCHF.OptInFastKPeriod, STOCHF.OptInFastDPeriod);
                                    break;

                                case (int)STOCHFOutputEnum.FastD:
                                    TALib.Core.StochF(high, low, close, startIdx, endIdx, new double[length], STOCHF.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHF.OptInFastDMAType, STOCHF.OptInFastKPeriod, STOCHF.OptInFastDPeriod);
                                    break;
                            }
                            break;

                        case IndicatorEnum.STOCH:
                            var STOCH = (STOCH)indicator;
                            TALib.Core.Stoch(high, low, close, startIdx, endIdx, STOCH.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCH.OptInSlowKMAType, (TALib.Core.MAType)STOCH.OptInSlowDMAType, STOCH.OptInFastKPeriod, STOCH.OptInSlowKPeriod, STOCH.OptInSlowDPeriod);
                            break;

                        case IndicatorEnum.STOCHRSI:
                            var STOCHRSI = (STOCHRSI)indicator;
                            switch (STOCHRSI.STOCHRSIOutput)
                            {
                                case (int)STOCHRSIOutputEnum.FastK:
                                    TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, open, high, low, close), startIdx, endIdx, STOCHRSI.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;

                                case (int)STOCHRSIOutputEnum.FastD:
                                    TALib.Core.StochRsi(BuildInRealArray(STOCHRSI.PriceType, open, high, low, close), startIdx, endIdx, new double[length], STOCHRSI.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;
                            }
                            break;

                        case IndicatorEnum.ULTOSC:
                            var ULTOSC = (ULTOSC)indicator;
                            TALib.Core.UltOsc(high, low, close, startIdx, endIdx, ULTOSC.Output, out OutBegIdx, out OutNBElement, ULTOSC.OptInTimePeriod1, ULTOSC.OptInTimePeriod2, ULTOSC.OptInTimePeriod3);
                            break;

                        case IndicatorEnum.VAR:
                            var VAR = (VAR)indicator;
                            TALib.Core.Var(BuildInRealArray(VAR.PriceType, open, high, low, close), startIdx, endIdx, VAR.Output, out OutBegIdx, out OutNBElement, VAR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.WILLR:
                            var WILLR = (WILLR)indicator;
                            TALib.Core.WillR(high, low, close, startIdx, endIdx, WILLR.Output, out OutBegIdx, out OutNBElement, WILLR.OptInTimePeriod);
                            break;
                    }

                    indicator.OutNBElement = OutNBElement;
                    indicator.OutBegIdx = OutBegIdx;

                    if (isExtraction)
                    {
                        indicator.IntervalLabels = (from candle in candlesRange
                                                    select new IntervalLabel
                                                    {
                                                        Interval = DateTimeHelper.BuildDateTime(timeframeId, candle.Date, candle.Time),
                                                        Label = candle.Label
                                                    }).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        private static double[] BuildInRealArray(int priceType, double[] open, double[] high, double[] low, double[] close)
        {
            return priceType switch
            {
                (int)PriceTypeEnum.LOW => low,
                (int)PriceTypeEnum.CLOSE => close,
                (int)PriceTypeEnum.OPEN => open,
                _ => high
            };
        }
    }
}

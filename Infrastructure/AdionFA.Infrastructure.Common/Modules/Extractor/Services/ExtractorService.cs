using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Mappers;
using AdionFA.Infrastructure.Enums;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AdionFA.Infrastructure.Common.Extractor.Mappers;
using AdionFA.Infrastructure.Common.Extensions;
using CsvHelper.Configuration;
using AdionFA.Infrastructure.Common.Base;
using AdionFA.Infrastructure.Common.Attributes;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.Infrastructure.Common.Logger.Helpers;
using AdionFA.Infrastructure.Common.Helpers;

namespace AdionFA.Infrastructure.Common.Extractor.Services
{
    public class ExtractorService : InfrastructureServiceBase, IExtractorService
    {
        public ExtractorService() : base()
        {
        }

        public List<IndicatorBase> BuildIndicatorsFromCSV(string path)
        {
            try
            {
                var records = new List<IndicatorBase>();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<ADXMap>();
                    csv.Context.RegisterClassMap<ADXRMap>();
                    csv.Context.RegisterClassMap<APOMap>();
                    csv.Context.RegisterClassMap<AROONMap>();
                    csv.Context.RegisterClassMap<ATRMap>();
                    //csv.Context.RegisterClassMap<BOPMap>();
                    csv.Context.RegisterClassMap<CCIMap>();
                    csv.Context.RegisterClassMap<CMOMap>();
                    csv.Context.RegisterClassMap<DXMap>();
                    csv.Context.RegisterClassMap<MACDMap>();
                    csv.Context.RegisterClassMap<MINUS_DIMap>();
                    csv.Context.RegisterClassMap<MINUS_DMMap>();
                    csv.Context.RegisterClassMap<MOMMap>();
                    csv.Context.RegisterClassMap<PLUS_DIMap>();
                    csv.Context.RegisterClassMap<PLUS_DMMap>();
                    csv.Context.RegisterClassMap<PPOMap>();
                    csv.Context.RegisterClassMap<ROCMap>();
                    csv.Context.RegisterClassMap<RSIMap>();
                    csv.Context.RegisterClassMap<STDDEVMap>();
                    csv.Context.RegisterClassMap<STOCHFMap>();
                    csv.Context.RegisterClassMap<STOCHMap>();
                    csv.Context.RegisterClassMap<STOCHRSIMap>();
                    csv.Context.RegisterClassMap<ULTOSCMap>();
                    csv.Context.RegisterClassMap<VARMap>();
                    csv.Context.RegisterClassMap<WILLRMap>();

                    while (csv.Read())
                    {
                        string typeCsv = csv.GetField(0).Split(';')[0];
                        if (Enum.TryParse(typeCsv, out IndicatorEnum indicatorType))
                        {
                            switch (indicatorType)
                            {
                                case IndicatorEnum.ADX:
                                    records.Add(csv.GetRecord<ADX>());
                                    break;
                                case IndicatorEnum.ADXR:
                                    records.Add(csv.GetRecord<ADXR>());
                                    break;
                                case IndicatorEnum.APO:
                                    records.Add(csv.GetRecord<APO>());
                                    break;
                                case IndicatorEnum.AROON:
                                    records.Add(csv.GetRecord<AROON>());
                                    break;
                                case IndicatorEnum.ATR:
                                    records.Add(csv.GetRecord<ATR>());
                                    break;
                                case IndicatorEnum.BOP:
                                    records.Add(new BOP());
                                    break;
                                case IndicatorEnum.CCI:
                                    records.Add(csv.GetRecord<CCI>());
                                    break;
                                case IndicatorEnum.CMO:
                                    records.Add(csv.GetRecord<CMO>());
                                    break;
                                case IndicatorEnum.DX:
                                    records.Add(csv.GetRecord<DX>());
                                    break;
                                case IndicatorEnum.MACD:
                                    records.Add(csv.GetRecord<MACD>());
                                    break;
                                case IndicatorEnum.MINUS_DI:
                                    records.Add(csv.GetRecord<MINUS_DI>());
                                    break;
                                case IndicatorEnum.MINUS_DM:
                                    records.Add(csv.GetRecord<MINUS_DM>());
                                    break;
                                case IndicatorEnum.MOM:
                                    records.Add(csv.GetRecord<MOM>());
                                    break;
                                case IndicatorEnum.PLUS_DI:
                                    records.Add(csv.GetRecord<PLUS_DI>());
                                    break;
                                case IndicatorEnum.PLUS_DM:
                                    records.Add(csv.GetRecord<PLUS_DM>());
                                    break;
                                case IndicatorEnum.PPO:
                                    records.Add(csv.GetRecord<PPO>());
                                    break;
                                case IndicatorEnum.ROC:
                                    records.Add(csv.GetRecord<ROC>());
                                    break;
                                case IndicatorEnum.RSI:
                                    records.Add(csv.GetRecord<RSI>());
                                    break;
                                case IndicatorEnum.STDDEV:
                                    records.Add(csv.GetRecord<STDDEV>());
                                    break;
                                case IndicatorEnum.STOCHF:
                                    records.Add(csv.GetRecord<STOCHF>());
                                    break;
                                case IndicatorEnum.STOCH:
                                    records.Add(csv.GetRecord<STOCH>());
                                    break;
                                case IndicatorEnum.STOCHRSI:
                                    records.Add(csv.GetRecord<STOCHRSI>());
                                    break;
                                case IndicatorEnum.ULTOSC:
                                    records.Add(csv.GetRecord<ULTOSC>());
                                    break;
                                case IndicatorEnum.VAR:
                                    records.Add(csv.GetRecord<VAR>());
                                    break;
                                case IndicatorEnum.WILLR:
                                    records.Add(csv.GetRecord<WILLR>());
                                    break;
                            }
                        }
                    }
                }

                return records;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public List<IndicatorBase> BuildIndicatorsFromNode(List<string> node)
        {
            try
            {
                List<IndicatorBase> indicators = new();
                foreach (string n in node)
                {
                    string f = n.Replace("|", string.Empty).Replace(" ", string.Empty);

                    MathOperatorEnum? optor = null;
                    string[] divisions = null;

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
                    if (divisions.Length == 2)
                    {
                        string[] indicatorParams = divisions[0].Split("_");

                        string indicatorName = indicatorParams[0].Replace(".", "_");

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

        public List<IndicatorBase> ExtractorBacktest(
            Candle firstCandle,
            Candle currentCandle,
            IEnumerable<IndicatorBase> indicators,
            IEnumerable<Candle> candles)
        {
            try
            {
                var extractions = new List<IndicatorBase>(indicators);

                var open = (from c in candles select c.Open).ToArray();
                var high = (from c in candles select c.High).ToArray();
                var low = (from c in candles select c.Low).ToArray();
                var close = (from c in candles select c.Close).ToArray();

                var startIdx = Array.IndexOf(candles.ToArray(), firstCandle);
                var endIdx = Array.IndexOf(candles.ToArray(), currentCandle);

                BacktestExtractor(startIdx, endIdx, open, high, low, close, extractions);

                return extractions;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<ExtractorService>(ex);
                throw;
            }
        }

        private static void BacktestExtractor(
            int startIdx,
            int endIdx,
            double[] open,
            double[] high,
            double[] low,
            double[] close,
            List<IndicatorBase> indicators)
        {
            try
            {
                int length = (endIdx - startIdx) + 1;
                foreach (var indicator in indicators)
                {
                    indicator.Output = new double[length];

                    int OutNBElement = 0;
                    int OutBegIdx = 0;

                    switch (indicator.Type)
                    {
                        case IndicatorEnum.ADX:
                            ADX ADX = (ADX)indicator;
                            TALib.Core.Adx(high, low, close, startIdx, endIdx, ADX.Output, out OutBegIdx, out OutNBElement, ADX.OptInTimePeriod);
                            break;

                        case IndicatorEnum.ADXR:
                            ADXR ADXR = (ADXR)indicator;
                            TALib.Core.Adxr(high, low, close, startIdx, endIdx, ADXR.Output, out OutBegIdx, out OutNBElement, ADXR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.APO:
                            APO APO = (APO)indicator;
                            TALib.Core.Apo(buildInRealArray(APO.PriceType), startIdx, endIdx, APO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)APO.MAType, APO.OptInFastPeriod, APO.OptInSlowPeriod);
                            break;

                        case IndicatorEnum.AROON:
                            AROON AROON = (AROON)indicator;

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
                            ATR ATR = (ATR)indicator;
                            TALib.Core.Atr(high, low, close, startIdx, endIdx, ATR.Output, out OutBegIdx, out OutNBElement, ATR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.BOP:
                            BOP BOP = (BOP)indicator;
                            TALib.Core.Bop(open, high, low, close, startIdx, endIdx, BOP.Output, out OutBegIdx, out OutNBElement);
                            break;

                        case IndicatorEnum.CCI:
                            CCI CCI = (CCI)indicator;
                            TALib.Core.Cci(high, low, close, startIdx, endIdx, CCI.Output, out OutBegIdx, out OutNBElement, CCI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.CMO:
                            CMO CMO = (CMO)indicator;
                            TALib.Core.Cmo(buildInRealArray(CMO.PriceType), startIdx, endIdx, CMO.Output, out OutBegIdx, out OutNBElement, CMO.OptInTimePeriod);
                            break;

                        case IndicatorEnum.DX:
                            DX DX = (DX)indicator;
                            TALib.Core.Dx(high, low, close, startIdx, endIdx, DX.Output, out OutBegIdx, out OutNBElement, DX.OptInTimePeriod);
                            break;

                        case IndicatorEnum.MACD:
                            MACD MACD = (MACD)indicator;

                            var macd = new double[length];
                            var signal = new double[length];
                            var hist = new double[length];

                            switch (MACD.MACDOutput)
                            {
                                case (int)MACDOutputEnum.Macd:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, MACD.Output, signal, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;

                                case (int)MACDOutputEnum.MacdSignal:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, macd, MACD.Output, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;

                                case (int)MACDOutputEnum.MacdHist:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, macd, signal, MACD.Output, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;
                            }
                            break;
                        case IndicatorEnum.MINUS_DI:
                            MINUS_DI MINUS_DI = (MINUS_DI)indicator;
                            TALib.Core.MinusDI(high, low, close, startIdx, endIdx, MINUS_DI.Output, out OutBegIdx, out OutNBElement, MINUS_DI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.MINUS_DM:
                            MINUS_DM MINUS_DM = (MINUS_DM)indicator;
                            TALib.Core.MinusDM(high, low, startIdx, endIdx, MINUS_DM.Output, out OutBegIdx, out OutNBElement, MINUS_DM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.MOM:
                            MOM MOM = (MOM)indicator;
                            TALib.Core.Mom(buildInRealArray(MOM.PriceType), startIdx, endIdx, MOM.Output, out OutBegIdx, out OutNBElement, MOM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PLUS_DI:
                            PLUS_DI PLUS_DI = (PLUS_DI)indicator;
                            TALib.Core.PlusDI(high, low, close, startIdx, endIdx, PLUS_DI.Output, out OutBegIdx, out OutNBElement, PLUS_DI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PLUS_DM:
                            PLUS_DM PLUS_DM = (PLUS_DM)indicator;
                            TALib.Core.PlusDM(high, low, startIdx, endIdx, PLUS_DM.Output, out OutBegIdx, out OutNBElement, PLUS_DM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PPO:
                            PPO PPO = (PPO)indicator;
                            TALib.Core.Ppo(buildInRealArray(PPO.PriceType), startIdx, endIdx, PPO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)PPO.MAType, PPO.OptInFastPeriod, PPO.OptInSlowPeriod);
                            break;

                        case IndicatorEnum.ROC:
                            ROC ROC = (ROC)indicator;
                            TALib.Core.Roc(buildInRealArray(ROC.PriceType), startIdx, endIdx, ROC.Output, out OutBegIdx, out OutNBElement, ROC.OptInTimePeriod);
                            break;

                        case IndicatorEnum.RSI:
                            RSI RSI = (RSI)indicator;
                            TALib.Core.Rsi(buildInRealArray(RSI.PriceType), startIdx, endIdx, RSI.Output, out OutBegIdx, out OutNBElement, RSI.OptInTimePeriod);
                            break;

                        case IndicatorEnum.STDDEV:
                            STDDEV STDDEV = (STDDEV)indicator;
                            TALib.Core.StdDev(buildInRealArray(STDDEV.PriceType), startIdx, endIdx, STDDEV.Output, out OutBegIdx, out OutNBElement, STDDEV.OptInTimePeriod, STDDEV.OptInNbDev);
                            break;

                        case IndicatorEnum.STOCHF:
                            STOCHF STOCHF = (STOCHF)indicator;
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
                            STOCH STOCH = (STOCH)indicator;
                            TALib.Core.Stoch(high, low, close, startIdx, endIdx, STOCH.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCH.OptInSlowKMAType, (TALib.Core.MAType)STOCH.OptInSlowDMAType, STOCH.OptInFastKPeriod, STOCH.OptInSlowKPeriod, STOCH.OptInSlowDPeriod);
                            break;

                        case IndicatorEnum.STOCHRSI:
                            STOCHRSI STOCHRSI = (STOCHRSI)indicator;
                            switch (STOCHRSI.STOCHRSIOutput)
                            {
                                case (int)STOCHRSIOutputEnum.FastK:
                                    TALib.Core.StochRsi(buildInRealArray(STOCHRSI.PriceType), startIdx, endIdx, STOCHRSI.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;

                                case (int)STOCHRSIOutputEnum.FastD:
                                    TALib.Core.StochRsi(buildInRealArray(STOCHRSI.PriceType), startIdx, endIdx, new double[length], STOCHRSI.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;
                            }
                            break;

                        case IndicatorEnum.ULTOSC:
                            ULTOSC ULTOSC = (ULTOSC)indicator;
                            TALib.Core.UltOsc(high, low, close, startIdx, endIdx, ULTOSC.Output, out OutBegIdx, out OutNBElement, ULTOSC.OptInTimePeriod1, ULTOSC.OptInTimePeriod2, ULTOSC.OptInTimePeriod3);
                            break;

                        case IndicatorEnum.VAR:
                            VAR VAR = (VAR)indicator;
                            TALib.Core.Var(buildInRealArray(VAR.PriceType), startIdx, endIdx, VAR.Output, out OutBegIdx, out OutNBElement, VAR.OptInTimePeriod);
                            break;

                        case IndicatorEnum.WILLR:
                            WILLR WILLR = (WILLR)indicator;
                            TALib.Core.WillR(high, low, close, startIdx, endIdx, WILLR.Output, out OutBegIdx, out OutNBElement, WILLR.OptInTimePeriod);
                            break;
                    }

                    indicator.OutNBElement = OutNBElement;
                    indicator.OutBegIdx = OutBegIdx;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            double[] buildInRealArray(int priceType)
            {
                var inReal = high;
                switch (priceType)
                {
                    case (int)PriceTypeEnum.LOW:
                        inReal = low;
                        break;

                    case (int)PriceTypeEnum.CLOSE:
                        inReal = close;
                        break;

                    case (int)PriceTypeEnum.OPEN:
                        inReal = open;
                        break;
                }
                return inReal;
            }
        }

        public List<IndicatorBase> ExtractorExecute(
            DateTime from,
            DateTime to,
            List<IndicatorBase> indicators,
            IEnumerable<Candle> candles,
            int timeframeId,
            decimal variation = 0)
        {
            try
            {
                if (timeframeId == 0)
                    throw new Exception(MessageResources.MarketDataPeriodCannotBeNull);

                var extractions = new List<IndicatorBase>(indicators);

                // Select candles that meet the variation condition
                var data = (from c in candles
                            let v = int.Parse(c.Open.ToString(CultureInfo.InvariantCulture).Replace(".", string.Empty)) - int.Parse(c.Close.ToString(CultureInfo.InvariantCulture).Replace(".", string.Empty))
                            where Math.Abs(v) >= (double)variation || variation == 0
                            select new Candle
                            {
                                Date = c.Date,
                                Time = c.Time,
                                Open = c.Open,
                                High = c.High,
                                Low = c.Low,
                                Close = c.Close,
                                Volume = c.Volume,
                                Label = v > 0 ? "DOWN" : "UP"
                            }).ToList();

                var open = (from c in data select c.Open).ToArray();
                var max = (from c in data select c.High).ToArray();
                var close = (from c in data select c.Close).ToArray();
                var min = (from c in data select c.Low).ToArray();

                // Select candles that are within the IS start and end dates
                IEnumerable<Candle> candlesRange = from c in data
                                                   let dt = DateTimeHelper.BuildDateTime(timeframeId, c.Date, c.Time)
                                                   where dt >= @from && dt <= to
                                                   select c;

                var startIdx = Array.IndexOf(data.ToArray(), candlesRange.FirstOrDefault());
                var endIdx = Array.IndexOf(data.ToArray(), candlesRange.LastOrDefault());

                ExtractorExecute(startIdx, endIdx, open, max, min, close, extractions, candlesRange, timeframeId);

                return extractions;
            }
            catch (Exception ex)
            {
                LogHelper.LogException<ExtractorService>(ex);
                throw;
            }
        }

        private static void ExtractorExecute(
            int startIdx,
            int endIdx,
            double[] open,
            double[] high,
            double[] low,
            double[] close,
            List<IndicatorBase> indicators,
            IEnumerable<Candle> candlesRange,
            int timeframeId)
        {
            try
            {
                int length = (endIdx - startIdx) + 1;
                foreach (var indicator in indicators)
                {
                    indicator.Output = new double[length];

                    int OutNBElement = 0;
                    int OutBegIdx = 0;
                    switch (indicator.Type)
                    {
                        case IndicatorEnum.ADX:
                            ADX ADX = (ADX)indicator;
                            TALib.Core.Adx(high, low, close, startIdx, endIdx, ADX.Output, out OutBegIdx, out OutNBElement, ADX.OptInTimePeriod);
                            break;
                        case IndicatorEnum.ADXR:
                            ADXR ADXR = (ADXR)indicator;
                            TALib.Core.Adxr(high, low, close, startIdx, endIdx, ADXR.Output, out OutBegIdx, out OutNBElement, ADXR.OptInTimePeriod);
                            break;
                        case IndicatorEnum.APO:
                            APO APO = (APO)indicator;
                            TALib.Core.Apo(buildInRealArray(APO.PriceType), startIdx, endIdx, APO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)APO.MAType, APO.OptInFastPeriod, APO.OptInSlowPeriod);
                            break;
                        case IndicatorEnum.AROON:
                            AROON AROON = (AROON)indicator;

                            double[] up = new double[length];
                            double[] down = new double[length];

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
                            ATR ATR = (ATR)indicator;
                            TALib.Core.Atr(high, low, close, startIdx, endIdx, ATR.Output, out OutBegIdx, out OutNBElement, ATR.OptInTimePeriod);
                            break;
                        case IndicatorEnum.BOP:
                            BOP BOP = (BOP)indicator;
                            TALib.Core.Bop(open, high, low, close, startIdx, endIdx, BOP.Output, out OutBegIdx, out OutNBElement);
                            break;
                        case IndicatorEnum.CCI:
                            CCI CCI = (CCI)indicator;
                            TALib.Core.Cci(high, low, close, startIdx, endIdx, CCI.Output, out OutBegIdx, out OutNBElement, CCI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.CMO:
                            CMO CMO = (CMO)indicator;
                            TALib.Core.Cmo(buildInRealArray(CMO.PriceType), startIdx, endIdx, CMO.Output, out OutBegIdx, out OutNBElement, CMO.OptInTimePeriod);
                            break;
                        case IndicatorEnum.DX:
                            DX DX = (DX)indicator;
                            TALib.Core.Dx(high, low, close, startIdx, endIdx, DX.Output, out OutBegIdx, out OutNBElement, DX.OptInTimePeriod);
                            break;
                        case IndicatorEnum.MACD:
                            MACD MACD = (MACD)indicator;

                            double[] macd = new double[length];
                            double[] signal = new double[length];
                            double[] hist = new double[length];

                            switch (MACD.MACDOutput)
                            {
                                case (int)MACDOutputEnum.Macd:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, MACD.Output, signal, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;
                                case (int)MACDOutputEnum.MacdSignal:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, macd, MACD.Output, hist, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;
                                case (int)MACDOutputEnum.MacdHist:
                                    TALib.Core.Macd(buildInRealArray(MACD.PriceType), startIdx, endIdx, macd, signal, MACD.Output, out OutBegIdx, out OutNBElement, MACD.OptInFastPeriod, MACD.OptInSlowPeriod, MACD.OptInSignalPeriod);
                                    break;
                            }
                            break;
                        case IndicatorEnum.MINUS_DI:
                            MINUS_DI MINUS_DI = (MINUS_DI)indicator;
                            TALib.Core.MinusDI(high, low, close, startIdx, endIdx, MINUS_DI.Output, out OutBegIdx, out OutNBElement, MINUS_DI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.MINUS_DM:
                            MINUS_DM MINUS_DM = (MINUS_DM)indicator;
                            TALib.Core.MinusDM(high, low, startIdx, endIdx, MINUS_DM.Output, out OutBegIdx, out OutNBElement, MINUS_DM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.MOM:
                            MOM MOM = (MOM)indicator;
                            TALib.Core.Mom(buildInRealArray(MOM.PriceType), startIdx, endIdx, MOM.Output, out OutBegIdx, out OutNBElement, MOM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PLUS_DI:
                            PLUS_DI PLUS_DI = (PLUS_DI)indicator;
                            TALib.Core.PlusDI(high, low, close, startIdx, endIdx, PLUS_DI.Output, out OutBegIdx, out OutNBElement, PLUS_DI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PLUS_DM:
                            PLUS_DM PLUS_DM = (PLUS_DM)indicator;
                            TALib.Core.PlusDM(high, low, startIdx, endIdx, PLUS_DM.Output, out OutBegIdx, out OutNBElement, PLUS_DM.OptInTimePeriod);
                            break;
                        case IndicatorEnum.PPO:
                            PPO PPO = (PPO)indicator;
                            TALib.Core.Ppo(buildInRealArray(PPO.PriceType), startIdx, endIdx, PPO.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)PPO.MAType, PPO.OptInFastPeriod, PPO.OptInSlowPeriod);
                            break;
                        case IndicatorEnum.ROC:
                            ROC ROC = (ROC)indicator;
                            TALib.Core.Roc(buildInRealArray(ROC.PriceType), startIdx, endIdx, ROC.Output, out OutBegIdx, out OutNBElement, ROC.OptInTimePeriod);
                            break;
                        case IndicatorEnum.RSI:
                            RSI RSI = (RSI)indicator;
                            TALib.Core.Rsi(buildInRealArray(RSI.PriceType), startIdx, endIdx, RSI.Output, out OutBegIdx, out OutNBElement, RSI.OptInTimePeriod);
                            break;
                        case IndicatorEnum.STDDEV:
                            STDDEV STDDEV = (STDDEV)indicator;
                            TALib.Core.StdDev(buildInRealArray(STDDEV.PriceType), startIdx, endIdx, STDDEV.Output, out OutBegIdx, out OutNBElement, STDDEV.OptInTimePeriod, STDDEV.OptInNbDev);
                            break;
                        case IndicatorEnum.STOCHF:
                            STOCHF STOCHF = (STOCHF)indicator;
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
                            STOCH STOCH = (STOCH)indicator;
                            TALib.Core.Stoch(high, low, close, startIdx, endIdx, STOCH.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCH.OptInSlowKMAType, (TALib.Core.MAType)STOCH.OptInSlowDMAType, STOCH.OptInFastKPeriod, STOCH.OptInSlowKPeriod, STOCH.OptInSlowDPeriod);
                            break;
                        case IndicatorEnum.STOCHRSI:
                            STOCHRSI STOCHRSI = (STOCHRSI)indicator;
                            switch (STOCHRSI.STOCHRSIOutput)
                            {
                                case (int)STOCHRSIOutputEnum.FastK:
                                    TALib.Core.StochRsi(buildInRealArray(STOCHRSI.PriceType), startIdx, endIdx, STOCHRSI.Output, new double[length], out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;
                                case (int)STOCHRSIOutputEnum.FastD:
                                    TALib.Core.StochRsi(buildInRealArray(STOCHRSI.PriceType), startIdx, endIdx, new double[length], STOCHRSI.Output, out OutBegIdx, out OutNBElement, (TALib.Core.MAType)STOCHRSI.OptInFastDMAType, STOCHRSI.OptInTimePeriod, STOCHRSI.OptInFastKPeriod, STOCHRSI.OptInFastDPeriod);
                                    break;
                            }
                            break;
                        case IndicatorEnum.ULTOSC:
                            ULTOSC ULTOSC = (ULTOSC)indicator;
                            TALib.Core.UltOsc(high, low, close, startIdx, endIdx, ULTOSC.Output, out OutBegIdx, out OutNBElement, ULTOSC.OptInTimePeriod1, ULTOSC.OptInTimePeriod2, ULTOSC.OptInTimePeriod3);
                            break;
                        case IndicatorEnum.VAR:
                            VAR VAR = (VAR)indicator;
                            TALib.Core.Var(buildInRealArray(VAR.PriceType), startIdx, endIdx, VAR.Output, out OutBegIdx, out OutNBElement, VAR.OptInTimePeriod);
                            break;
                        case IndicatorEnum.WILLR:
                            WILLR WILLR = (WILLR)indicator;
                            TALib.Core.WillR(high, low, close, startIdx, endIdx, WILLR.Output, out OutBegIdx, out OutNBElement, WILLR.OptInTimePeriod);
                            break;
                    }
                    indicator.OutNBElement = OutNBElement;
                    indicator.OutBegIdx = OutBegIdx;
                    indicator.IntervalLabels = (from cr in candlesRange
                                                select new IntervalLabel
                                                {
                                                    Interval = DateTimeHelper.BuildDateTime(timeframeId, cr.Date, cr.Time),
                                                    Label = cr.Label
                                                }).ToArray();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            double[] buildInRealArray(int priceType)
            {
                double[] inReal = high;
                switch (priceType)
                {
                    case (int)PriceTypeEnum.LOW:
                        inReal = low;
                        break;

                    case (int)PriceTypeEnum.CLOSE:
                        inReal = close;
                        break;

                    case (int)PriceTypeEnum.OPEN:
                        inReal = open;
                        break;
                }
                return inReal;
            }
        }

        public bool ExtractorWrite(string path, List<IndicatorBase> indicators, int fromRegionTime = 0, int toRegionTime = 0)
        {
            try
            {
                if (indicators.Count > 0)
                {
                    int columnCount = indicators.Count;
                    int rowCount = indicators.FirstOrDefault().IntervalLabels.Where(
                        il => (fromRegionTime == 0 || il.Interval.Hour >= fromRegionTime) &&
                                (toRegionTime == 0 || il.Interval.Hour <= toRegionTime)
                    ).ToList().Count;

                    using var writer = new StreamWriter(path);
                    using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                    string[] headers = new string[columnCount];
                    object[,] items = new object[columnCount, rowCount];
                    int iCounter = 0;
                    foreach (var i in indicators)
                    {
                        PropertyInfo[] properties = i.GetType().GetFilteredProperties<IgnoreReflectionAttribute>();

                        var parameters = string.Join("_", (from p in properties
                                                           where p.PropertyType.Name == typeof(int).Name || p.PropertyType.Name == typeof(double).Name
                                                           select p.GetValue(i)).ToList());

                        headers[iCounter] = $"{i.Name.Replace("_", ".")}_{parameters}";

                        if (i.Output.Length > 0)
                        {
                            int zzz = 0;
                            for (int jCounter = 0; jCounter < i.Output.Length; jCounter++)
                            {
                                DateTime dt = i.IntervalLabels[jCounter].Interval;
                                if ((fromRegionTime == 0 || dt.Hour >= fromRegionTime) && (toRegionTime == 0 || dt.Hour <= toRegionTime))
                                {
                                    items[iCounter, zzz] = i.Output[jCounter];
                                    zzz++;
                                }
                            }
                        }
                        iCounter++;
                    }

                    //Identify header duplicated
                    var duplicates = headers.Select((t, i) => new { Index = i, Text = t })
                        .GroupBy(g => g.Text).Where(g => g.Count() > 1);

                    List<int> indexDuplicates = new();

                    //Build csv
                    csv.WriteField("Id");
                    csv.WriteField("Fecha-Hora");
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var h = headers[i];
                        var d = duplicates.FirstOrDefault(_d => _d.Key == h);
                        if (d == null || d.FirstOrDefault().Index == i)
                        {
                            csv.WriteField(h);
                        }
                        else
                        {
                            indexDuplicates.Add(i);
                        }
                    }
                    csv.WriteField("LABEL");
                    csv.NextRecord();

                    IntervalLabel[] intervals = indicators.FirstOrDefault().IntervalLabels.Where(
                        il => (fromRegionTime == 0 || il.Interval.Hour >= fromRegionTime) &&
                                (toRegionTime == 0 || il.Interval.Hour <= toRegionTime)
                    ).ToArray();

                    for (int i = 0; i < rowCount; i++)
                    {
                        csv.WriteField(i);
                        csv.WriteField(intervals[i].Interval.ToString("yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture));
                        for (int j = 0; j < columnCount; j++)
                        {
                            if (!indexDuplicates.Contains(j))
                            {
                                object record = Math.Round((double)items[j, i], 8);
                                csv.WriteField(record);
                            }
                        }
                        csv.WriteField(intervals[i].Label);
                        csv.NextRecord();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            return false;
        }

        public IEnumerable<Candle> GetCandles(string filePath)
        {
            try
            {
                List<Candle> result = new();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                };

                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, config);

                csv.Context.RegisterClassMap<CandleMap>();

                var candles = csv.GetRecords<Candle>();

                foreach (var r in candles)
                {
                    result.Add(r);
                }

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

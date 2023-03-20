using Adion.FinancialAutomat.Core.API.Contracts.Markets;
using Adion.FinancialAutomat.Core.API.Contracts.Projects;
using Adion.FinancialAutomat.Infrastructure.Common.Extractor.Contracts;
using Adion.FinancialAutomat.Infrastructure.Common.Extractor.Model;
using Adion.FinancialAutomat.Infrastructure.Common.IofC;
using Adion.FinancialAutomat.Infrastructure.Common.Weka.Model;
using Adion.FinancialAutomat.Infrastructure.Common.Weka.Services;
using Adion.FinancialAutomat.Infrastructure.Core.IofCExt;
using Adion.FinancialAutomat.Infrastructure.Enums;
using Adion.FinancialAutomat.TransferObject.Market;
using Adion.FinancialAutomat.TransferObject.Project;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Indicators
{
    class Program
    {
        static async Task Main(string[] args)
        {
            new IoC().Setup();

            #region History
            IProjectAPI projectService = IoC.Get<IProjectAPI>();
            IMarketDataAPI historyService = IoC.Get<IMarketDataAPI>();
            IExtractorService extractorService = IoC.Get<IExtractorService>();

            ProjectDTO project = projectService.GetProject(1, false);
            ProjectConfigurationDTO pconfig = projectService.GetProjectConfiguration(project.ProjectId, true);
            MarketDataDTO history = historyService.GetMarketData(pconfig.MarketDataId ?? 0, true);

            IEnumerable<Candle> candles = history.MarketDataDetails.Select(
                    h => new Candle
                    {
                        date = h.StartDate,
                        time = h.StartTime,
                        open = h.OpenPrice,
                        max = h.MaxPrice,
                        min = h.MinPrice,
                        close = h.ClosePrice,
                        volumen = h.Volumen
                    }
                ).OrderBy(d => d.date).ThenBy(d => d.time).ToList();
            #endregion

            #region Weka
            string sourcePath = @"C:\Users\guerr\OneDrive\Documents\FA.Workspace\Projects\EUR.USD.1h\Extractions\\WithoutSchedule\Ext-010957.2021.01.17.19.37.28.csv";

            var httpClient = new WekaApiClient();
            HttpOperationResponse<IList<REPTreeOutputModel>> response =
                await httpClient.GetREPTreeClassifierWithHttpMessagesAsync
                (
                    sourcePath, 15, 8, 100, 10000000, 1, 1, 100
                );

            if (response.Response.IsSuccessStatusCode)
            {
                IList<REPTreeOutputModel> result = response.Body;

                foreach (var n in result.SelectMany(m => m.NodeOutput.Where(n => n.Winner)))
                {
                    int operations = 0;
                    int operationWinner = 0;
                    int operationLosser = 0;

                    List<IndicatorBase> indicators = BuildIndicatorsFromNode(n.Node);

                    DateTime today = DateTime.UtcNow;
                    DateTime fromDateIS = today.AddYears(-4);
                    DateTime toDateOS = today.AddYears(1);

                    List<IndicatorBase> extractorResult = extractorService.ExtractorExecute(fromDateIS, today, indicators, candles, 0);
                    int nodeTotalRules = extractorResult.Count; 
                    if (nodeTotalRules > 0)
                    {
                        var temporalIndicator = extractorResult.FirstOrDefault();
                        int length = temporalIndicator.Output.Length;
                        int counter = 0;
                        
                        while (counter < length)
                        {
                            int passed = 0;
                            string upOrDown = temporalIndicator.IntervalLabels[counter].Label;

                            foreach (var indicator in extractorResult)
                            {
                                double output = indicator.Output[counter];

                                switch (indicator.Operator)
                                {
                                    case MathOperatorEnum.GreaterThanOrEqual:
                                        passed += indicator.Value >= output ? 1 : 0;
                                        break;
                                    case MathOperatorEnum.LessThanOrEqual:
                                        passed += indicator.Value <= output ? 1 : 0;
                                        break;
                                    case MathOperatorEnum.GreaterThan:
                                        passed += indicator.Value > output ? 1 : 0;
                                        break;
                                    case MathOperatorEnum.LessThan:
                                        passed += indicator.Value > output ? 1 : 0;
                                        break;
                                    case MathOperatorEnum.Equal:
                                        passed += indicator.Value == output ? 1 : 0;
                                        break;
                                }
                            }

                            if (passed == nodeTotalRules)
                            {
                                operations++;

                                if (n.Label == upOrDown)
                                {
                                    operationWinner++;
                                }
                                else
                                {
                                    operationLosser++;
                                }
                            }

                            counter++;
                        }
                    }
                    
                }
            }
            #endregion
        }

        static List<IndicatorBase> BuildIndicatorsFromNode(List<string> node)
        {
            try
            {
                List<IndicatorBase> indicators = new List<IndicatorBase>();
                foreach (string n in node)
                {
                    string f = n.Replace("|", string.Empty).Replace(" ", string.Empty);
                    Console.WriteLine($"{f}");
                    MathOperatorEnum? optor = null; 
                    string[] divisions = null;

                    #region Operator Split
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
                    else if (f.Contains(">"))
                    {
                        optor = MathOperatorEnum.GreaterThan;
                        divisions = f.Split(">");
                    }
                    else if (f.Contains("<"))
                    {
                        optor = MathOperatorEnum.LessThan;
                        divisions = f.Split("<");
                    }
                    else if (f.Contains("="))
                    {
                        optor = MathOperatorEnum.Equal;
                        divisions = f.Split("=");
                    }
                    #endregion

                    #region Params Split
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
                    #endregion
                }

                return indicators;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Extractor.Contracts;
using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts;
using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Services
{
    public class TradeService : ITradeService
    {
        #region Services

        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IExtractorService ExtractorService;

        #endregion

        #region Ctor

        public TradeService()
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            ExtractorService = IoC.Get<IExtractorService>();
        }

        #endregion

        public bool IsTrade(CurrencyPeriodEnum period, AssembledBuilderModel model, IEnumerable<Candle> candles)
        {
            /*
            List<IndicatorBase> indicators = ExtractorService.BuildIndicatorsFromNode(node);

            DateTime fromDateIS = candles.LastOrDefault().date.AddSeconds(-(period).ToSeconds());
            DateTime toDateOS = candles.LastOrDefault().date.AddSeconds((period).ToSeconds());
            List<IndicatorBase> extractorResult = ExtractorService.ExtractorExecute(fromDateIS, toDateOS, indicators, candles, 0);

            int nodeTotalRules = extractorResult.Count;
            if (nodeTotalRules > 0)
            {
                var temporalIndicator = extractorResult.FirstOrDefault();
                int length = temporalIndicator.Output.Length;

                int counter = 0;
                while (counter < length)
                {
                    int passed = 0;

                    foreach (var indicator in extractorResult)
                    {
                        double output = indicator.Output[counter];

                        switch (indicator.Operator)
                        {
                            case MathOperatorEnum.GreaterThanOrEqual:
                                passed += output >= indicator.Value ? 1 : 0;
                                break;
                            case MathOperatorEnum.LessThanOrEqual:
                                passed += output <= indicator.Value ? 1 : 0;
                                break;
                            case MathOperatorEnum.GreaterThan:
                                passed += output > indicator.Value ? 1 : 0;
                                break;
                            case MathOperatorEnum.LessThan:
                                passed += output < indicator.Value ? 1 : 0;
                                break;
                            case MathOperatorEnum.Equal:
                                passed += output == indicator.Value ? 1 : 0;
                                break;
                        }
                    }

                    if (passed == nodeTotalRules)
                    {
                        return true;
                    }

                    counter++;
                }
            }
            */
            return false;
        }

        public string OpenOperationMessage()
        {
            var request = new ZmqMsgRequestModel
            {
                UUID = Guid.NewGuid().ToString(),
                SYMBOL = "EURUSD",
                Request = "TRADE",
                OrderType = "BUY",
                Action = "CLOSE_ALL"
            };

            return request.Message;
        }

        public string CloseAllOperationMessage()
        {
            var request = new ZmqMsgRequestModel
            {
                UUID = Guid.NewGuid().ToString(),
                SYMBOL = "EURUSD",
                Request = "TRADE",
                OrderType = string.Empty,
                Action = "CLOSE_ALL"
            };

            return request.Message;
        }
    }
}

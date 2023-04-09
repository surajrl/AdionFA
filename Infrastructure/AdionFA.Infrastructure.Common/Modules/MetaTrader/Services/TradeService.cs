using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Services
{
    public class TradeService : ITradeService
    {
        private readonly IExtractorService _extractorService;

        public TradeService()
        {
            _extractorService = IoC.Get<IExtractorService>();
        }

        public bool IsTrade(
            TimeframeEnum period,
            BacktestModel model,
            IEnumerable<Candle> candles)
        {
            List<IndicatorBase> indicators = _extractorService.BuildIndicatorsFromNode(model.Node.ToList());

            DateTime from = candles.LastOrDefault().Date.AddSeconds(-period.ToSeconds());
            DateTime to = candles.LastOrDefault().Date.AddSeconds(period.ToSeconds());

            List<IndicatorBase> extractorResult = _extractorService.ExtractorExecute(
                from,
                to,
                indicators,
                candles,
                (int)period,
                metaTraderTest: true);

            if (extractorResult.Any())
            {
                int totalRules = extractorResult.Count;
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

                    if (passed == totalRules)
                    {
                        return true;
                    }

                    counter++;
                }
            }

            return false;
        }

        public ZmqMsgRequestModel OpenOperation(OrderTypeEnum buyOrSell = OrderTypeEnum.Buy)
        {
            var request = new ZmqMsgRequestModel
            {
                UUID = Guid.NewGuid().ToString(),
                Action = "TRADE_ACTION_DEAL",   // Place a trade order for an immediate execution with the specified parameters(market order)
                Symbol = "EURUSD",
                OrderType = buyOrSell,
            };

            return request;
        }

        public ZmqMsgRequestModel CloseAllOperation()
        {
            var request = new ZmqMsgRequestModel
            {
                UUID = Guid.NewGuid().ToString(),
                Action = "TRADE_ACTION_CLOSE_BY",   // Close a position by an opposite one
                OrderType = OrderTypeEnum.Close,    // Order to close a position by an opposite one
            };

            return request;
        }
    }
}

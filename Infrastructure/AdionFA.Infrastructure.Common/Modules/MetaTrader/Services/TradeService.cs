using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using AdionFA.Infrastructure.Common.Weka.Model;

namespace AdionFA.Infrastructure.Common.MetaTrader.Services
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
            REPTreeNodeModel model,
            IEnumerable<Candle> candles)
        {
            var indicators = _extractorService.BuildIndicatorsFromNode(model.Node.ToList());

            var extractorResult = _extractorService.ExtractorMetaTrader(
                candles.LastOrDefault(),
                indicators,
                candles);

            if (!extractorResult.Any())
            {
                return false;
            }

            var totalRules = extractorResult.Count;
            var passed = 0;

            foreach (var indicator in extractorResult)
            {
                var output = indicator.Output[0];

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

            return passed == totalRules;
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

using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.MetaTrader.Services
{
    public class TradeService : ITradeService
    {
        private readonly IExtractorService _extractorService;

        public TradeService()
        {
            _extractorService = IoC.Get<IExtractorService>();
        }


        public ZmqMsgRequestModel OperationRequest(OrderTypeEnum orderType)
        {
            return new ZmqMsgRequestModel
            {
                OrderType = orderType,
            };
        }

        public bool IsTrade(
            IList<string> node,
            IList<Candle> candleHistory,
            Candle currentCandle)
        {
            candleHistory.Add(currentCandle);

            var nodeIndicators = _extractorService.BuildIndicatorsFromNode(node.ToList());
            var nodeIndicatorsResult = _extractorService.CalculateNodeIndicators(
                candleHistory.FirstOrDefault(),
                currentCandle,
                nodeIndicators,
                candleHistory.ToList());

            // Check each indicator
            for (var i = 0; i < nodeIndicatorsResult.Count; i++)
            {
                var indicator = nodeIndicatorsResult[i];

                if (indicator.OutNBElement == 0)
                {
                    candleHistory.Remove(currentCandle);
                    return false;
                }

                var output = indicator.Output[indicator.OutNBElement - 1];

                switch (indicator.Operator)
                {
                    case MathOperatorEnum.GreaterThanOrEqual:
                        if (output >= indicator.Value)
                        {
                            break;
                        }

                        candleHistory.Remove(currentCandle);
                        return false;

                    case MathOperatorEnum.LessThanOrEqual:
                        if (output <= indicator.Value)
                        {
                            break;
                        }

                        candleHistory.Remove(currentCandle);
                        return false;

                    case MathOperatorEnum.GreaterThan:
                        if (output > indicator.Value)
                        {
                            break;
                        }

                        candleHistory.Remove(currentCandle);
                        return false;

                    case MathOperatorEnum.LessThan:
                        if (output < indicator.Value)
                        {
                            break;
                        }

                        candleHistory.Remove(currentCandle);
                        return false;

                    case MathOperatorEnum.Equal:
                        if (output == indicator.Value)
                        {
                            break;
                        }

                        candleHistory.Remove(currentCandle);
                        return false;
                }
            }

            candleHistory.Remove(currentCandle);
            return true;
        }
    }
}
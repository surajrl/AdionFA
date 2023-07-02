using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using System;
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

        public bool IsTrade(
            AssemblyNodeModel assembledNode,
            IList<Candle> candleHistory,
            Candle currentCandle)
        {
            candleHistory.Add(currentCandle);
            if (IsTrade(assembledNode.ParentNodeData.Node, candleHistory, currentCandle))
            {
                // Try every child node until one is met
                foreach (var childNode in assembledNode.ChildNodesData)
                {
                    if (IsTrade(childNode, candleHistory, currentCandle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsTrade(
            REPTreeNodeModel singleNode,
            IList<Candle> candleHistory,
            Candle currentCandle)
        {
            candleHistory.Add(currentCandle);
            return IsTrade(singleNode.Node, candleHistory, currentCandle);
        }

        public ZmqMsgRequestModel OpenOperation(OrderTypeEnum orderType)
        {
            var request = new ZmqMsgRequestModel
            {
                UUID = Guid.NewGuid().ToString(),
                OrderType = orderType,
            };

            return request;
        }

        private bool IsTrade(
            IList<string> node,
            IList<Candle> candleHistory,
            Candle currentCandle)
        {
            var nodeIndicators = _extractorService.BuildIndicatorsFromNode(node.ToList());
            var nodeIndicatorsResult = _extractorService.CalculateNodeIndicators(
                candleHistory.FirstOrDefault(),
                currentCandle,
                nodeIndicators,
                candleHistory.ToList());

            if (!nodeIndicatorsResult.Any())
            {
                return false;
            }

            // Check each indicator
            for (var i = 0; i < nodeIndicatorsResult.Count; i++)
            {
                var indicator = nodeIndicatorsResult[i];

                if (indicator.OutNBElement == 0)
                {
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
                        return false;

                    case MathOperatorEnum.LessThanOrEqual:
                        if (output <= indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.GreaterThan:
                        if (output > indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.LessThan:
                        if (output < indicator.Value)
                        {
                            break;
                        }
                        return false;

                    case MathOperatorEnum.Equal:
                        if (output == indicator.Value)
                        {
                            break;
                        }
                        return false;
                }
            }

            return true;
        }
    }
}
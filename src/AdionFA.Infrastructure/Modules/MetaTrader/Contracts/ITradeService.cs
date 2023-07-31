using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.MetaTrader.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.MetaTrader.Contracts
{
    public interface ITradeService
    {
        ZmqMsgRequestModel OperationRequest(OrderTypeEnum orderType);

        bool IsTrade(IList<string> singleNode, IList<Candle> candleHistory, Candle currentCandle);
    }
}
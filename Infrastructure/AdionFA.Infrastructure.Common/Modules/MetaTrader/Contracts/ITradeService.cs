using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Enums;

using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.MetaTrader.Contracts
{
    public interface ITradeService
    {
        ZmqMsgRequestModel OpenOperation(OrderTypeEnum orderType);

        bool IsTrade(IList<string> singleNode, IList<Candle> candleHistory, Candle currentCandle);
    }
}
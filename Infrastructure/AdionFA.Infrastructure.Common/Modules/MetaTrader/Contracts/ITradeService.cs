using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;

using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.MetaTrader.Contracts
{
    public interface ITradeService
    {
        bool IsTrade(REPTreeNodeModel singleNode, IList<Candle> candleHistory, Candle currentCandle);
        bool IsTrade(AssembledNodeModel assembledNode, IList<Candle> candleHistory, Candle currentCandle);

        ZmqMsgRequestModel OpenOperation(OrderTypeEnum orderType);
    }
}
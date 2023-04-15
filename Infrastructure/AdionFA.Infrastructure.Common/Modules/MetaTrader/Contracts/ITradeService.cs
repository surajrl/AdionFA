using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;

using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.MetaTrader.Contracts
{
    public interface ITradeService
    {
        bool IsTrade(TimeframeEnum period, REPTreeNodeModel node, IEnumerable<Candle> candles);

        ZmqMsgRequestModel OpenOperation(OrderTypeEnum buyOrSell = OrderTypeEnum.Buy);

        ZmqMsgRequestModel CloseAllOperation();
    }
}

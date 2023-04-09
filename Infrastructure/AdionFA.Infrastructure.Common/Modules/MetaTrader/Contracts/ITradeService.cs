using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts
{
    public interface ITradeService
    {
        bool IsTrade(TimeframeEnum period, BacktestModel node, IEnumerable<Candle> candles);

        ZmqMsgRequestModel OpenOperation(OrderTypeEnum buyOrSell = OrderTypeEnum.Buy);

        ZmqMsgRequestModel CloseAllOperation();
    }
}

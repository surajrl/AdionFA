using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using AdionFA.Infrastructure.Enums;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts
{
    public interface ITradeService
    {
        bool IsTrade(TimeframeEnum period, AssembledBuilderModel node, IEnumerable<Candle> candles);

        string OpenOperationMessage();

        string CloseAllOperationMessage();
    }
}

using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model;
using Adion.FA.Infrastructure.Enums;
using System.Collections.Generic;

namespace Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts
{
    public interface ITradeService
    {
        bool IsTrade(CurrencyPeriodEnum period, AssembledBuilderModel node, IEnumerable<Candle> candles);

        string OpenOperationMessage();

        string CloseAllOperationMessage();
    }
}

using Adion.FA.UI.Station.Modules.Trader.Model;
using System;

namespace Adion.FA.UI.Station.Modules.Trader.Services
{
    public interface IMarketDataService
    {
        IObservable<MarketData> Watch(string currencyPair);
    }
}

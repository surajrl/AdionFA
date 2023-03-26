using AdionFA.UI.Station.Modules.Trader.Model;
using System;

namespace AdionFA.UI.Station.Modules.Trader.Services
{
    public interface IMarketDataService
    {
        IObservable<MarketData> Watch(string currencyPair);
    }
}

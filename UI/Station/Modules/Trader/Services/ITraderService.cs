using AdionFA.UI.Station.Modules.Trader.Model;
using DynamicData;

namespace AdionFA.UI.Station.Modules.Trader.Services
{
    public interface ITraderService
    {
        IObservableCache<Trade, long> All { get; }
        IObservableCache<Trade, long> Live { get; }
    }
}

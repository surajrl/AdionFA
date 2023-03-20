using Adion.FA.UI.Station.Modules.Trader.Model;
using DynamicData;

namespace Adion.FA.UI.Station.Modules.Trader.Services
{
    public interface ITraderService
    {
        IObservableCache<Trade, long> All { get; }
        IObservableCache<Trade, long> Live { get; }
    }
}

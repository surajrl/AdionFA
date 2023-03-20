using Adion.FA.UI.Station.Infrastructure.Contracts.Services;
using Adion.FA.UI.Station.Modules.Trader.Model;
using Adion.FA.UI.Station.Modules.Trader.Services;
using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Adion.FA.UI.Station.Modules.Trader.ViewModels
{
    public class TraderPositionViewModel : IDisposable
    {
        private readonly ReadOnlyObservableCollection<CurrencyPairPosition> _data;
        private readonly IDisposable _cleanUp;

        
        public TraderPositionViewModel(
            ITraderService tradeService, 
            ISchedulerProvider schedulerProvider)
        {
            _cleanUp = tradeService.Live.Connect()
                .Group(trade => trade.CurrencyPair)
                .Transform(group => new CurrencyPairPosition(group))
                .Sort(SortExpressionComparer<CurrencyPairPosition>.Ascending(t => t.CurrencyPair))
                .ObserveOn(schedulerProvider.MainThread)
                .Bind(out _data)
                .DisposeMany()
                .Subscribe();
        }
        

        public ReadOnlyObservableCollection<CurrencyPairPosition> Data => _data;
        
        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}

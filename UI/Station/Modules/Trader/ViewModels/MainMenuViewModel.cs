using AdionFA.UI.Station.Modules.Trader.Enums;
using AdionFA.UI.Station.Modules.Trader.Infrastructure;
using AdionFA.UI.Station.Modules.Trader.Model;
using DynamicData.Binding;
using Prism.Ioc;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AdionFA.UI.Station.Modules.Trader.ViewModels
{
    public class MainMenuViewModel : AbstractNotifyPropertyChanged, IDisposable
    {
        private readonly IEnumerable<MenuItem> _menuItems;
        public readonly ISubject<ViewContainer> _viewCreatedSubject = new Subject<ViewContainer>();

        private readonly IDisposable _cleanUp;
        private bool _showLinks = false;
        private MenuCategory _category = MenuCategory.DynamicData;
        private IEnumerable<MenuItem> _items;

        public MainMenuViewModel()
        {
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Trading Positions",
                       "Calculate overall position for each currency pair and aggregate totals",
                        () => Open<TraderPositionViewModel>("Trading Positions"),new []
                    {
                        new Link("View Model", "PositionsViewer.cs","https://github.com/RolandPheasant/Dynamic.Trader/blob/master/Trader.Client/Views/PositionsViewer.cs"),
                        new Link("Group Model", "CurrencyPairPosition.cs","https://github.com/RolandPheasant/Dynamic.Trader/blob/master/Trader.Domain/Model/CurrencyPairPosition.cs"),
                    }),
            };

            var filterApplier = this.WhenValueChanged(t => t.Category)
                .Subscribe(value =>
                {
                    Items = _menuItems.Where(menu => menu.Category == value).ToArray();
                });

            _cleanUp = Disposable.Create(() =>
            {
                _viewCreatedSubject.OnCompleted();
                filterApplier.Dispose();
            });
        }

        private void Open<T>(string title)
        {
            var content = ContainerLocator.Current.Resolve<T>();
            _viewCreatedSubject.OnNext(new ViewContainer(title, content));
        }

        private void OpenRxUI<T>(string title) where T : ReactiveObject
        {
            var content = ContainerLocator.Current.Resolve<T>();
            var rxuiContent = new RxUiHostViewModel(content);

            _viewCreatedSubject.OnNext(new ViewContainer(title, rxuiContent));
        }

        public MenuCategory Category
        {
            get => _category;
            set => SetAndRaise(ref _category, value);
        }

        public IEnumerable<MenuItem> Items
        {
            get => _items;
            set => SetAndRaise(ref _items, value);
        }

        public bool ShowLinks
        {
            get => _showLinks;
            set => SetAndRaise(ref _showLinks, value);
        }

        public IObservable<ViewContainer> ItemCreated => _viewCreatedSubject.AsObservable();

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}

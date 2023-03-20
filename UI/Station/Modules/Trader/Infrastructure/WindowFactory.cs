using Adion.FA.UI.Station.Modules.Trader.ViewModels;
using Adion.FA.UI.Station.Modules.Trader.Views;
using Dragablz;
using Prism.Ioc;
using System;
using System.Windows;

namespace Adion.FA.UI.Station.Modules.Trader.Infrastructure
{
    public class WindowFactory : IWindowFactory
    {
        public TraderTabWindow Create(bool showMenu = false)
        {
            var window = new TraderTabWindow();
            var model = ContainerLocator.Current.Resolve<TraderViewModel>();
            ///*if (showMenu) */model.ShowMenu();

            window.DataContext = model;

            window.Closing += (sender, e) =>
            {
                if (TabablzControl.GetIsClosingAsPartOfDragOperation(window)) return;

                var todispose = ((TraderView)sender).DataContext as IDisposable;
                todispose?.Dispose();
            };

            return window;
        }
    }
}

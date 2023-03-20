using Adion.FA.UI.Station.Modules.Trader.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adion.FA.UI.Station.Modules.Trader.Infrastructure
{
    public interface IWindowFactory
    {
        TraderTabWindow Create(bool showMenu = false);
    }
}

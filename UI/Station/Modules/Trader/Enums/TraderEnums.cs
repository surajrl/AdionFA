using System;
using System.Collections.Generic;
using System.Text;

namespace AdionFA.UI.Station.Modules.Trader.Enums
{
    public enum BuyOrSell
    {
        Buy,
        Sell
    }

    public enum TradeStatus
    {
        Live = 0,
        Closed = 1,
        Filled = 2
    }

    public enum MenuCategory
    {
        ReactiveUi,
        DynamicData
    }
}

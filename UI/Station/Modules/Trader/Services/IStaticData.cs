using AdionFA.UI.Station.Modules.Trader.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdionFA.UI.Station.Modules.Trader.Services
{
    public interface IStaticData
    {
        string[] Customers { get; }
        CurrencyPair[] CurrencyPairs { get; }
    }
}

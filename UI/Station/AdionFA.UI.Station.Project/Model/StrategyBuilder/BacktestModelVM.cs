using AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model;
using AdionFA.UI.Station.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Model.StrategyBuilder
{
    public class BacktestModelVM : ViewModelBase
    {
        public BacktestModel BacktestModel { get; set; }

        private bool _hasTestInMetaTrader;
        public bool HasTestInMetaTrader
        {
            get => _hasTestInMetaTrader;
            set => SetProperty(ref _hasTestInMetaTrader, value);
        }
    }
}

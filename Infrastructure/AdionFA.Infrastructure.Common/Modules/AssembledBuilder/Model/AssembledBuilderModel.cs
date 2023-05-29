using AdionFA.Infrastructure.Common.StrategyBuilder.Model;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class AssembledBuilderModel
    {
        public IList<BacktestModel> UPBacktests { get; } = new List<BacktestModel>();
        public IList<BacktestModel> DOWNBacktests { get; } = new List<BacktestModel>();
    }
}
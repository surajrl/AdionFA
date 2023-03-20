using System.Collections.Generic;
using System.Linq;

namespace Adion.FA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model
{
    public class CorrelationModel
    {
        public bool Success => BacktestUP.Any() || BacktestDOWN.Any();

        public CorrelationModel()
        {
            BacktestUP = new List<BacktestModel>();
            BacktestDOWN = new List<BacktestModel>();
        }

        public List<BacktestModel> BacktestUP { get; set; }
        public List<BacktestModel> BacktestDOWN { get; set; }
    }
}

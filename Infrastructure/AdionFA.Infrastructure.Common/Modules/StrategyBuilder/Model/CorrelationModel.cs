using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.StrategyBuilder.Model
{
    public class CorrelationModel
    {
        public bool Success => ISBacktestUP.Any() || ISBacktestDOWN.Any();

        public CorrelationModel()
        {
            ISBacktestUP = new List<BacktestModel>();
            ISBacktestDOWN = new List<BacktestModel>();
        }

        public List<BacktestModel> ISBacktestUP { get; set; }
        public List<BacktestModel> ISBacktestDOWN { get; set; }
    }
}

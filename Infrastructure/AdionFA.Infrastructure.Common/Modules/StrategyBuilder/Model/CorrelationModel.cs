using AdionFA.Infrastructure.Common.Weka.Model;
using System.Collections.Generic;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Infrastructures.StrategyBuilder.Model
{
    public class CorrelationModel
    {
        public bool Success => ISBacktestUP.Any() || ISBacktestDOWN.Any();

        public CorrelationModel()
        {
            ISBacktestUP = new List<BacktestModel>();
            ISBacktestDOWN = new List<BacktestModel>();

            BacktestUP = new List<REPTreeNodeModel>();
            BacktestDOWN = new List<REPTreeNodeModel>();
        }

        public List<BacktestModel> ISBacktestUP { get; set; }
        public List<BacktestModel> ISBacktestDOWN { get; set; }

        public List<REPTreeNodeModel> BacktestUP { get; set; }
        public List<REPTreeNodeModel> BacktestDOWN { get; set; }
    }
}

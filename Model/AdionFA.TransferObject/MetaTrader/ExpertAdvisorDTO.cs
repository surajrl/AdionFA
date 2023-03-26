using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;

namespace AdionFA.TransferObject.MetaTrader
{
    public class ExpertAdvisorDTO : EntityBaseDTO
    {
        #region Properties
        public int ExpertAdvisorId { get; set; }

        public string Name { get; set; }
        public string Protocol { get; set; }
        public string HostName { get; set; }
        public int REPPort { get; set; }
        public int PUSHPort { get; set; }
        public int Timer { get; set; }
        public int MagicNumber { get; set; }
        public int MaximumOrders { get; set; }
        public double MaximumLotSize { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }
        #endregion

        #region Nagevation Properties

        #endregion
    }
}

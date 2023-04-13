using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;

namespace AdionFA.TransferObject.MetaTrader
{
    public class ExpertAdvisorDTO : EntityBaseDTO
    {
        public int ExpertAdvisorId { get; set; }

        public string Name { get; set; }
        public string MagicNumber { get; set; }
        public string Host { get; set; }
        public string ResponsePort { get; set; }
        public string PushPort { get; set; }

        public ProjectDTO Project { get; set; }
        public int ProjectId { get; set; }
    }
}

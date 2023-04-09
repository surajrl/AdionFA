using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Project;

namespace AdionFA.TransferObject.MetaTrader
{
    public class ExpertAdvisorDTO : EntityBaseDTO
    {
        public int ExpertAdvisorId { get; set; }

        public string Name { get; set; }
        public ulong MagicNumber { get; set; }
        public string Host { get; set; }
        public ushort ResponsePort { get; set; }
        public ushort PushPort { get; set; }

        public ProjectDTO Project { get; set; }
        public int ProjectId { get; set; }
    }
}

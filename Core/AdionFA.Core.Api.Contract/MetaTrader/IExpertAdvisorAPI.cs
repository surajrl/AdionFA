using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Core.API.Contracts.MetaTrader
{
    public interface IExpertAdvisorAPI
    {
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisor);
        ExpertAdvisorDTO GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false);
        ExpertAdvisorDTO GetExpertAdvisor(int? projectId = null, bool includeGraph = false);
        ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisor);
    }
}

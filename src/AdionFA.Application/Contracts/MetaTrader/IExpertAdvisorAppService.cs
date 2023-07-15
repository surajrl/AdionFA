using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Application.Contracts.MetaTrader
{
    public interface IExpertAdvisorAppService : IAppContractBase
    {
        ExpertAdvisorDTO GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false);
        ExpertAdvisorDTO GetExpertAdvisor(int? projectId = null, bool includeGraph = false);
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisor);
        ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisor);
    }
}

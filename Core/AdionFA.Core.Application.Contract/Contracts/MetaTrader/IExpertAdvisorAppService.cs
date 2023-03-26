using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Core.Application.Contract.Contracts.MetaTrader
{
    public interface IExpertAdvisorAppService : IAppContractBase
    {
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor);
        ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}

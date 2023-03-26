using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Core.API.Contract.Contracts.MetaTrader
{
    public interface IExpertAdvisorAPI
    {
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor);
        ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}

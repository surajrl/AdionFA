using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.MetaTrader;

namespace Adion.FA.Core.API.Contract.Contracts.MetaTrader
{
    public interface IExpertAdvisorAPI
    {
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor);
        ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}

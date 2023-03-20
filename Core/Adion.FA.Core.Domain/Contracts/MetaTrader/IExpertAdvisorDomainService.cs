using Adion.FA.Core.Domain.Aggregates.MetaTrader;

namespace Adion.FA.Core.Domain.Contracts.MetaTrader
{
    public interface IExpertAdvisorDomainService
    {
        int? CreateExpertAdvisor(ExpertAdvisor advisor);
        ExpertAdvisor GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}

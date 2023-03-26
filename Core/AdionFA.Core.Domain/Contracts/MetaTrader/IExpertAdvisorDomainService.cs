using AdionFA.Core.Domain.Aggregates.MetaTrader;

namespace AdionFA.Core.Domain.Contracts.MetaTrader
{
    public interface IExpertAdvisorDomainService
    {
        int? CreateExpertAdvisor(ExpertAdvisor advisor);
        ExpertAdvisor GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}

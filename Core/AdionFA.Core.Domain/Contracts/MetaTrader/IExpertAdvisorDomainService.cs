using AdionFA.Core.Domain.Aggregates.MetaTrader;

namespace AdionFA.Core.Domain.Contracts.MetaTrader
{
    public interface IExpertAdvisorDomainService
    {
        int? CreateExpertAdvisor(ExpertAdvisor expertAdvisor);
        bool UpdateExpertAdvisor(ExpertAdvisor expertAdvisor);
        ExpertAdvisor GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false);
        ExpertAdvisor GetExpertAdvisor(int? projectId = null, bool includeGraph = false);
    }
}

using AdionFA.Domain.Entities;

namespace AdionFA.Domain.MetaTrader
{
    public interface IExpertAdvisorDomainService
    {
        int? CreateExpertAdvisor(ExpertAdvisor expertAdvisor);
        bool UpdateExpertAdvisor(ExpertAdvisor expertAdvisor);
        ExpertAdvisor GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false);
        ExpertAdvisor GetExpertAdvisor(int? projectId = null, bool includeGraph = false);
    }
}

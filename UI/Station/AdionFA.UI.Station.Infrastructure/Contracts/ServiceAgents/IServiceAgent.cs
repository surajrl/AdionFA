using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface IServiceAgent
    {
        // Expert Advisor

        Task<ExpertAdvisorVM> GetExpertAdvisor(int expertAdvisorId, int projectId, bool includeGraph = false);
        Task<ExpertAdvisorVM> GetExpertAdvisor(int projectId, bool includeGraph = false);
        Task<ResponseVM> CreateExpertAdvisor(ExpertAdvisorVM expertAdvisor);
        Task<ResponseVM> UpdateExpertAdvisor(ExpertAdvisorVM expertAdvisor);
    }
}

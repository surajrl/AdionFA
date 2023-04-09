using AdionFA.Core.API.Contracts.MetaTrader;
using AdionFA.Core.Application.Contracts.MetaTrader;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Core.API.MetaTrader
{
    public class ExpertAdvisorAPI : IExpertAdvisorAPI
    {
        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            using (var service = IoC.Get<IExpertAdvisorAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateExpertAdvisor(expertAdvisor);
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false)
        {
            using var service = IoC.Get<IExpertAdvisorAppService>();
            return service.GetExpertAdvisor(expertAdvisorId, projectId, includeGraph);
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? projectId = null, bool includeGraph = false)
        {
            using var service = IoC.Get<IExpertAdvisorAppService>();
            return service.GetExpertAdvisor(projectId, includeGraph);
        }

        public ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            using (var service = IoC.Get<IExpertAdvisorAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateExpertAdvisor(expertAdvisor);
            }
        }
    }
}

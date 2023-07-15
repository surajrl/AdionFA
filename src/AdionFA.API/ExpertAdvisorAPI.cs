using AdionFA.API.Contracts;
using AdionFA.Application.Contracts.MetaTrader;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.Persistance.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;
using Ninject;

namespace AdionFA.API
{
    public class ExpertAdvisorAPI : IExpertAdvisorAPI
    {
        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            using (var service = IoC.Kernel.Get<IExpertAdvisorAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateExpertAdvisor(expertAdvisor);
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IExpertAdvisorAppService>();
            return service.GetExpertAdvisor(expertAdvisorId, projectId, includeGraph);
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? projectId = null, bool includeGraph = false)
        {
            using var service = IoC.Kernel.Get<IExpertAdvisorAppService>();
            return service.GetExpertAdvisor(projectId, includeGraph);
        }

        public ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            using (var service = IoC.Kernel.Get<IExpertAdvisorAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.UpdateExpertAdvisor(expertAdvisor);
            }
        }
    }
}

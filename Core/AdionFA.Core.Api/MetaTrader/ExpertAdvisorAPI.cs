using AdionFA.Core.API.Contract.Contracts.MetaTrader;
using AdionFA.Core.Application.Contract.Contracts.MetaTrader;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

namespace AdionFA.Core.API.MetaTrader
{
    public class ExpertAdvisorAPI : IExpertAdvisorAPI
    {
        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor)
        {
            using (var service = IoC.Get<IExpertAdvisorAppService>())
            using (service.Transaction<IAdionFADbContext>())
            {
                return service.CreateExpertAdvisor(advisor);
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false)
        {
            using (var service = IoC.Get<IExpertAdvisorAppService>())
                return service.GetExpertAdvisor(advisorId, projectId, includeGraph);
        }
    }
}

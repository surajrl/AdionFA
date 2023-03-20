using Adion.FA.Core.API.Contract.Contracts.MetaTrader;
using Adion.FA.Core.Application.Contract.Contracts.MetaTrader;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.MetaTrader;

namespace Adion.FA.Core.API.MetaTrader
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

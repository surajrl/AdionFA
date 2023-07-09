using AdionFA.Core.Application.Contracts.MetaTrader;
using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Core.Domain.Contracts.MetaTrader;

using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

using Ninject;

using System;
using System.Diagnostics;

namespace AdionFA.Core.Application.Services.MetaTrader
{
    public class ExpertAdvisorAppService : AppServiceBase, IExpertAdvisorAppService
    {
        [Inject]
        public IExpertAdvisorDomainService ExpertAdvisorDomainService { get; set; }

        public ExpertAdvisorAppService()
            : base()
        {
        }

        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if (expertAdvisor != null)
                {
                    var ea = Mapper.Map<ExpertAdvisor>(expertAdvisor);

                    response.IsSuccess = ExpertAdvisorDomainService.CreateExpertAdvisor(ea) > 0;
                }

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? expertAdvisorId, int? projectId = null, bool includeGraph = false)
        {
            try
            {
                var ea = ExpertAdvisorDomainService.GetExpertAdvisor(expertAdvisorId, projectId, includeGraph);
                var dto = Mapper.Map<ExpertAdvisorDTO>(ea);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? projectId = null, bool includeGraph = false)
        {
            try
            {
                var ea = ExpertAdvisorDomainService.GetExpertAdvisor(projectId, includeGraph);
                var dto = Mapper.Map<ExpertAdvisorDTO>(ea);

                return dto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisor)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };
                var ea = Mapper.Map<ExpertAdvisor>(expertAdvisor);

                response.IsSuccess = ExpertAdvisorDomainService.UpdateExpertAdvisor(ea);

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

using AdionFA.Core.Application.Contract.Contracts.MetaTrader;
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
        #region Domain Service

        [Inject]
        public IExpertAdvisorDomainService ExpertAdvisorDomainService { get; set; }

        #endregion

        #region Ctor

        public ExpertAdvisorAppService() : base()
        {
        }

        #endregion

        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if (advisor != null)
                {
                    ExpertAdvisor ea = Mapper.Map<ExpertAdvisor>(advisor);

                    response.IsSuccess = ExpertAdvisorDomainService.CreateExpertAdvisor(ea) > 0;

                    if (response.IsSuccess)
                    {
                        LogInfoCreate<ExpertAdvisorDTO>();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                LogException<ExpertAdvisorAppService>(ex);
                throw; 
            }
        }

        public ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false)
        {
            try
            {
                ExpertAdvisor ea = ExpertAdvisorDomainService.GetExpertAdvisor(advisorId, projectId, includeGraph);
                ExpertAdvisorDTO dto = Mapper.Map<ExpertAdvisorDTO>(ea);

                return dto;
            }
            catch (Exception ex)
            {
                LogException<ExpertAdvisorAppService>(ex);
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

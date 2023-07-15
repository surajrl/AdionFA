using AdionFA.API.Contracts;
using AdionFA.Infrastructure.IofC;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;
using AdionFA.UI.Station.Infrastructure.AutoMapper;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
using AutoMapper;
using Ninject;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Services.AppServices
{
    public class ServiceAgent : IServiceAgent
    {
        public IMapper Mapper { get; set; }

        public ServiceAgent()
        {
            Mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();
        }

        // Expert Advisor

        public async Task<ResponseVM> CreateExpertAdvisor(ExpertAdvisorVM expertAdvisor)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };
                var dto = Mapper.Map<ExpertAdvisorVM, ExpertAdvisorDTO>(expertAdvisor);

                await Task.Run(() =>
                {
                    response = IoC.Kernel.Get<IExpertAdvisorAPI>().CreateExpertAdvisor(dto);
                });

                return Mapper.Map<ResponseDTO, ResponseVM>(response);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ExpertAdvisorVM> GetExpertAdvisor(int projectId, bool includeGraph = false)
        {
            try
            {
                ExpertAdvisorVM ea = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Kernel.Get<IExpertAdvisorAPI>().GetExpertAdvisor(projectId, includeGraph);

                    ea = Mapper.Map<ExpertAdvisorDTO, ExpertAdvisorVM>(dto);
                });

                return ea;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<ResponseVM> UpdateExpertAdvisor(ExpertAdvisorVM expertAdvisor)
        {
            var response = new ResponseDTO { IsSuccess = false };

            await Task.Run(() =>
            {
                var dto = Mapper.Map<ExpertAdvisorVM, ExpertAdvisorDTO>(expertAdvisor);
                response = IoC.Kernel.Get<IExpertAdvisorAPI>().UpdateExpertAdvisor(dto);
            });

            return Mapper.Map<ResponseDTO, ResponseVM>(response);
        }

        public async Task<ExpertAdvisorVM> GetExpertAdvisor(int expertAdvisorId, int projectId, bool includeGraph = false)
        {
            try
            {
                ExpertAdvisorVM ea = null;
                await Task.Run(() =>
                {
                    var dto = IoC.Kernel.Get<IExpertAdvisorAPI>().GetExpertAdvisor(expertAdvisorId, projectId, includeGraph);

                    ea = Mapper.Map<ExpertAdvisorDTO, ExpertAdvisorVM>(dto);
                });

                return ea;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

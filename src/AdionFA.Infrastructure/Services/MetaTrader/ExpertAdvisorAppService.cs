using AdionFA.Application.Contracts.MetaTrader;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities;
using AdionFA.Domain.Exceptions;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AdionFA.Application.Services.MetaTrader
{
    public class ExpertAdvisorAppService : AppServiceBase, IExpertAdvisorAppService
    {
        private IGenericRepository<ExpertAdvisor> _expertAdvisorRepository;

        public ExpertAdvisorAppService(IGenericRepository<ExpertAdvisor> expertAdvisorRepository)
            : base()
        {
            _expertAdvisorRepository = expertAdvisorRepository;
        }

        public ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO expertAdvisorDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };

                if (expertAdvisorDTO != null)
                {
                    var expertAdvisor = Mapper.Map<ExpertAdvisor>(expertAdvisorDTO);

                    if (expertAdvisor != null)
                    {
                        if (expertAdvisor.PublisherPort == expertAdvisor.ResponsePort)
                        {
                            throw new PropertiesWithSameValueAdionException();
                        }

                        _expertAdvisorRepository.Create(expertAdvisor);
                    }

                    response.IsSuccess = expertAdvisor.ExpertAdvisorId > 0;
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
                Expression<Func<ExpertAdvisor, bool>> predicate = p => p.ExpertAdvisorId == expertAdvisorId || p.ProjectId == projectId;

                var includes = new List<Expression<Func<ExpertAdvisor, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(ea => ea.Project);
                }

                var expertAdvisor = _expertAdvisorRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<ExpertAdvisorDTO>(expertAdvisor);
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
                Expression<Func<ExpertAdvisor, bool>> predicate = p => p.ProjectId == projectId;

                var includes = new List<Expression<Func<ExpertAdvisor, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(ea => ea.Project);
                }

                var expertAdvisor = _expertAdvisorRepository.FirstOrDefault(predicate, includes.ToArray());

                return Mapper.Map<ExpertAdvisorDTO>(expertAdvisor);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ResponseDTO UpdateExpertAdvisor(ExpertAdvisorDTO expertAdvisorDTO)
        {
            try
            {
                var response = new ResponseDTO { IsSuccess = false };
                var expertAdvisor = Mapper.Map<ExpertAdvisor>(expertAdvisorDTO);

                var ea = _expertAdvisorRepository.FirstOrDefault(ea => ea.ProjectId == expertAdvisor.ProjectId);

                if (ea is not null && ea.ExpertAdvisorId > 0)
                {
                    expertAdvisor.ExpertAdvisorId = ea.ExpertAdvisorId;
                    _expertAdvisorRepository.Update(expertAdvisor);
                    response.IsSuccess = true;
                }

                _expertAdvisorRepository.Create(expertAdvisor);
                response.IsSuccess = true;

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

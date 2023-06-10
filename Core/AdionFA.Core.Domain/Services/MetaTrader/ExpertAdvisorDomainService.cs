using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Core.Domain.Contracts.MetaTrader;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AdionFA.Core.Domain.Services.MetaTrader
{
    public class ExpertAdvisorDomainService : DomainServiceBase, IExpertAdvisorDomainService
    {
        private IRepository<ExpertAdvisor> _expertAdvisorRepository;

        public ExpertAdvisorDomainService(string ownerId, string owner, IRepository<ExpertAdvisor> expertAdvisorRepository)
            : base(ownerId, owner)
        {
            _expertAdvisorRepository = expertAdvisorRepository;
        }

        public int? CreateExpertAdvisor(ExpertAdvisor advisor)
        {
            int? eaId = null;
            try
            {
                if (advisor != null)
                {
                    if (advisor.PublisherPort == advisor.ResponsePort) throw new PropertiesWithSameValueAdionException();

                    _expertAdvisorRepository.Create(advisor);
                    eaId = advisor.ExpertAdvisorId;
                }

                return eaId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ExpertAdvisor GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false)
        {
            try
            {
                Expression<Func<ExpertAdvisor, bool>> predicate = p => p.ExpertAdvisorId == advisorId || p.ProjectId == projectId;

                var includes = new List<Expression<Func<ExpertAdvisor, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(ea => ea.Project);
                }

                return _expertAdvisorRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool UpdateExpertAdvisor(ExpertAdvisor expertAdvisor)
        {
            try
            {
                var ea = _expertAdvisorRepository.FirstOrDefault(ea => ea.ProjectId == expertAdvisor.ProjectId);

                if (ea is not null && ea.ExpertAdvisorId > 0)
                {
                    expertAdvisor.ExpertAdvisorId = ea.ExpertAdvisorId;
                    _expertAdvisorRepository.Update(expertAdvisor);
                    return true;
                }

                _expertAdvisorRepository.Create(expertAdvisor);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ExpertAdvisor GetExpertAdvisor(int? projectId = null, bool includeGraph = false)
        {
            try
            {
                Expression<Func<ExpertAdvisor, bool>> predicate = p => p.ProjectId == projectId;

                var includes = new List<Expression<Func<ExpertAdvisor, dynamic>>> { };
                if (includeGraph)
                {
                    includes.Add(ea => ea.Project);
                }

                return _expertAdvisorRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}

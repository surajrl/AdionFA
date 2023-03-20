using Adion.FA.Core.Domain.Aggregates.MetaTrader;
using Adion.FA.Core.Domain.Contracts.MetaTrader;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Core.Domain.Exceptions.MetaTrader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Adion.FA.Core.Domain.Services.MetaTrader
{
    public class ExpertAdvisorDomainService : DomainServiceBase, IExpertAdvisorDomainService
    {
        #region Repositories
        public IRepository<ExpertAdvisor> ExpertAdvisorRepository { get; set; }
        #endregion

        #region Ctor
        public ExpertAdvisorDomainService(string tenantId, string ownerId, string owner,
            IRepository<ExpertAdvisor> expertAdvisorRepository) : base(tenantId, ownerId, owner)
        {
            ExpertAdvisorRepository = expertAdvisorRepository;
        }
        #endregion

        #region ExpertAdvisor
        public int? CreateExpertAdvisor(ExpertAdvisor advisor)
        {
            int? eaId = null;
            try
            {
                if(advisor != null)
                {
                    if(advisor.PUSHPort == advisor.REPPort) throw new PropertiesWithSameValueAdionException();

                    ExpertAdvisorRepository.Create(advisor);
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
                
                var includes = new List<Expression<Func<ExpertAdvisor, dynamic>>>{ };
                if (includeGraph)
                {
                    includes.Add(ea => ea.Project);
                }

                return ExpertAdvisorRepository.FirstOrDefault(predicate, includes.ToArray());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion
    }
}

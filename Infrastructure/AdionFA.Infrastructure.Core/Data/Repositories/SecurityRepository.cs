using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Core.Data.Persistence;
using AdionFA.Infrastructure.Core.Data.Persistence.EFCore;

namespace AdionFA.Infrastructure.Core.Data.Repositories
{
    public class SecurityRepository<TEntity> : RepositoryBase<TEntity>, ISecurityRepository<TEntity> where TEntity : EntityBase
    {
        public SecurityRepository(IUnitOfWork<AdionSecurityDbContext> unitOfWork)
        {
            DatabaseContext = ((UnitOfWork<AdionSecurityDbContext>)unitOfWork)._context;

            _tenantId = unitOfWork._tenantId;
            _ownerId = unitOfWork._ownerId;
            _owner = unitOfWork._owner;
        }
    }
}

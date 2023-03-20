using Adion.FA.Core.Domain.Aggregates.Base;
using Adion.FA.Core.Domain.Contracts.Repositories;
using Adion.FA.Infrastructure.Core.Data.Persistence;
using Adion.FA.Infrastructure.Core.Data.Persistence.EFCore;

namespace Adion.FA.Infrastructure.Core.Data.Repositories
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

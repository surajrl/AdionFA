using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Infrastructure.Core.Data.Persistence;

namespace AdionFA.Infrastructure.Core.Data.Repositories
{
    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityBase
    {
        public Repository(IUnitOfWork<AdionFADbContext> unitOfWork)
        {
            DatabaseContext = ((UnitOfWork<AdionFADbContext>)unitOfWork)._context;
            
            _tenantId = unitOfWork._tenantId;
            _ownerId = unitOfWork._ownerId;
            _owner = unitOfWork._owner;
        }
    }
}

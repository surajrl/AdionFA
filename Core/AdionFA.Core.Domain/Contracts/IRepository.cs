using AdionFA.Core.Domain.Aggregates.Base;

namespace AdionFA.Core.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
    }
}

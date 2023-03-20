using Adion.FA.Core.Domain.Aggregates.Base;

namespace Adion.FA.Core.Domain.Contracts.Repositories
{
    public interface ISecurityRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
    }
}

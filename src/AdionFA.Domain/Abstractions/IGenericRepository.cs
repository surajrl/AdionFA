using AdionFA.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdionFA.Domain.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities, bool softDelete);
        Task DeleteAsync(TEntity entity, bool softDelete);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] includes);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes);
    }
}

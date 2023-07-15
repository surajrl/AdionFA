using AdionFA.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Domain.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        void Create(TEntity entity, bool autoSave = true);
        void Update(TEntity entity, bool autoSave = true);
        void Delete(IEnumerable<TEntity> entities, bool autoSave = true);
        void Delete(TEntity entity, bool autoSave = true);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] includes);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes);

        TEntity LastTemporalRecord();
    }
}

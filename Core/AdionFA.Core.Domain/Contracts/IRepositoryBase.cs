using AdionFA.Core.Domain.Contracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Core.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<T> : IIdentityBase where T : class
    {
        void Create(T entity, bool autoSave = true);
        void Update(T entity, bool autoSave = true);
        void Delete(IEnumerable<T> entities, bool softDelete = true);
        void Delete(T entity, bool softDelete = true);
        bool Any(Expression<Func<T, bool>> predicate);

        IQueryable<T> AsNoTracking();


        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params string[] include);
        IQueryable<T> GetAll(params Expression<Func<T, dynamic>>[] includes);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, dynamic>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params string[] includes);

        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(params Expression<Func<T, dynamic>>[] includes);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, dynamic>>[] includes);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includes);

        T LastTemporalRecord();

        void SaveChanges();
    }
}

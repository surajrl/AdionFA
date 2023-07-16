using AdionFA.Domain.Contracts.Bases;
using AdionFA.Domain.Contracts.Repositories;
using AdionFA.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdionFA.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private readonly string _id = "0";
        private readonly string _username = "admin";

        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Create(TEntity entity)
        {
            entity.CreatedById = _id;
            entity.CreatedByUserName = _username;
            entity.CreatedOn = DateTime.UtcNow;

            entity.IsDeleted = false;

            _dbContext.Set<TEntity>().Add(entity);

            _dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdatedById = _id;
            entity.UpdatedByUserName = _username;
            entity.UpdatedOn = DateTime.UtcNow;

            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var attachedEntity = _dbContext.Set<TEntity>().Find(entity.Id());
                if (attachedEntity != null)
                {
                    _dbContext.Entry(attachedEntity).CurrentValues.SetValues(entity);
                }
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<TEntity> entities, bool softDelete = true)
        {
            foreach (var entity in entities)
            {
                Delete(entity, softDelete);
            }
        }

        public virtual void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete)
            {
                entity.IsDeleted = true;
                Update(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        // Get All

        public virtual IQueryable<TEntity> GetAll()
        {
            var query = _dbContext.Set<TEntity>().Where(e => e.IsDeleted == false);
            return query;
        }

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = GetAll();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetAll();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = GetAll(predicate);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        // First Or Default

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetAll(predicate);

            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = GetAll(predicate, includes);

            return query.FirstOrDefault();
        }

        // Temporal Patterns

        public TEntity LastTemporalRecord()
        {
            if (Activator.CreateInstance(typeof(TEntity)) is ITimeSensitive)
            {
                return FirstOrDefault(t => ((t as ITimeSensitive).EndDate ?? DateTime.MinValue) == DateTime.MinValue);
            }

            return null;
        }
    }
}

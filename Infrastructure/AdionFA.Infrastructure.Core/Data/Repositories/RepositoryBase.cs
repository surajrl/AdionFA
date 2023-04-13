using AdionFA.Core.Domain.Aggregates.Base;
using AdionFA.Core.Domain.Contracts.Bases;
using AdionFA.Core.Domain.Contracts.Repositories;
using AdionFA.Core.Domain.Extensions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AdionFA.Infrastructure.Core.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        // Identity

        public string _tenantId { get; set; }
        public string _ownerId { get; set; }
        public string _owner { get; set; }

        // Database

        protected DbContext DatabaseContext;

        public virtual void Create(TEntity entity, bool autoSave = true)
        {
            try
            {
                entity.TenantId = _tenantId;
                entity.CreatedById = _ownerId;
                entity.CreatedByUserName = _owner;

                entity.IsDeleted = false;
                entity.Inaccesible = false;
                entity.CreatedOn = DateTime.UtcNow;

                DatabaseContext.Set<TEntity>().Add(entity);

                if (autoSave)
                    DatabaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public virtual void Update(TEntity entity, bool autoSave = true)
        {
            entity.UpdatedById = _ownerId;
            entity.UpdatedByUserName = _owner;
            entity.UpdatedOn = DateTime.UtcNow;

            var entry = DatabaseContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var attachedEntity = DatabaseContext.Set<TEntity>().Find(entity.GetId());
                if (attachedEntity != null)
                {
                    DatabaseContext.Entry(attachedEntity).CurrentValues.SetValues(entity);
                }
            }
            else
            {
                DatabaseContext.Entry(entity).State = EntityState.Modified;
            }

            if (autoSave)
                DatabaseContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<TEntity> entities, bool softDelete = true)
        {
            foreach (TEntity entity in entities)
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
                DatabaseContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetAll(predicate);
            return query.Any();
        }

        // Get All

        public virtual IQueryable<TEntity> AsNoTracking()
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().AsNoTracking().Where(e => e.IsDeleted == false);
            return query;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().Where(e => e.IsDeleted == false);
            return query;
        }

        public virtual IQueryable<TEntity> GetAll(params string[] include)
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().Where(e => e.IsDeleted == false);
            if (include != null && include.Any())
            {
                query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = GetAll();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = GetAll();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = GetAll(predicate);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = GetAll(predicate);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        // FirstOrDefault

        public virtual TEntity FirstOrDefault()
        {
            IQueryable<TEntity> query = GetAll();
            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = GetAll(predicate);
            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = GetAll(includes);
            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = GetAll(predicate, includes);
            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = GetAll(predicate, includes);
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

        // Save

        public void SaveChanges()
        {
            DatabaseContext.SaveChanges();
        }
    }
}

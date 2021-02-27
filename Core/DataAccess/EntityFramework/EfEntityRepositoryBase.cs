using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); 
                addedEntity.State = EntityState.Added;
                context.SaveChanges();

            };
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);  
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

            };
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); 
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            };
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                #region if version
                //if (filter == null)
                //{
                //    return context.Set<Product>().ToList();
                //}
                //else
                //{
                //    return context.Set<Product>().Where(filter).ToList();
                //}  //todo: sonradan dön bak!!!
                #endregion

                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> exp)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Any(exp);
            }
        }

        public TEntity GetByID(int id)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public object Select(Expression<Func<TEntity, bool>> exp)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Select(exp).ToList();
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> exp)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(exp);
            }
        }
    }
}

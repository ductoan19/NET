using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.Add(entity);
            item.CreateAt = DateTime.Now;
            item.CreateBy = "";
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return base.Add(entity);
        }
        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.AddAsync(entity, cancellationToken);
            item.CreateAt = DateTime.Now;
            item.CreateBy = "";
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return base.AddAsync(entity, cancellationToken);
        }
        public override void AddRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                var item = entity as BaseModel;
                if (item == null)
                {
                    base.Add(entity);
                }
                else
                {
                    item.CreateAt = DateTime.Now;
                    item.CreateBy = "";
                    item.UpdateAt = DateTime.Now;
                    item.UpdateBy = "";
                }
            }
            base.AddRange(entities);
        }
        public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                var item = entity as BaseModel;
                if (item == null)
                {
                    base.AddAsync(entity);
                }
                else
                {
                    item.CreateAt = DateTime.Now;
                    item.CreateBy = "";
                    item.UpdateAt = DateTime.Now;
                    item.UpdateBy = "";
                }
            }
            return base.AddRangeAsync(entities, cancellationToken);
        }
        public override EntityEntry Update(object entity)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.Update(entity);
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return base.Update(item);
        }
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.Update(entity);
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return base.Update<TEntity>(entity);
        }
        public override void UpdateRange(IEnumerable<object> entities)
        {
            base.UpdateRange(entities);
        }
        public override EntityEntry Remove(object entity)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.Remove(entity);
            item.IsDeleted = true;
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return Update(item);
        }
        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            var item = entity as BaseModel;
            if (item == null)
                return base.Remove(entity);

            item.IsDeleted = true;
            item.UpdateAt = DateTime.Now;
            item.UpdateBy = "";
            return Update<TEntity>(entity);
        }
        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

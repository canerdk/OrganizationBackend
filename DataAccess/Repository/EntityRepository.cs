using DataAccess.Context;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly AppDbContext _context;

        public EntityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await _context.AddAsync(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _context.AddRangeAsync(entity);
            var count = await _context.SaveChangesAsync();
            return count;
        }

        public async Task<TEntity> SoftDeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                var softDeletedEntity = _context.Update(entity);

                //await UpdateRelatedEntitiesSoftDelete(entity);

                await _context.SaveChangesAsync();

                return softDeletedEntity.Entity;
            }

            return entity;
        }

        public async Task<TEntity> HardDeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                var hardDeletedEntity = _context.Remove(entity);
                hardDeletedEntity.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return hardDeletedEntity.Entity;
            }

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, CancellationToken cancellationToken = default, bool asNoTracking = true)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            queryable = queryable.Where(x => x.IsDeleted == false);

            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);

            if (orderBy != null) queryable = orderBy(queryable);
            else queryable = queryable.OrderByDescending(x => x.CreatedDate);

            if (asNoTracking) queryable = queryable.AsNoTracking();

            return await queryable.ToListAsync(cancellationToken);
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate);
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Update(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            _context.UpdateRange(entity);
            var count = await _context.SaveChangesAsync();
            return count;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (predicate != null)
                return await queryable.Where(predicate).CountAsync();
            else
                return await queryable.CountAsync();
        }


        private async Task UpdateRelatedEntitiesSoftDelete(TEntity entity)
        {
            foreach (var navigationEntry in _context.Entry(entity).Navigations)
            {
                await navigationEntry.LoadAsync();

                dynamic relatedEntities = navigationEntry.CurrentValue;

                foreach (var relatedEntity in relatedEntities)
                {
                    var isDeletedProperty = relatedEntity.GetType().GetProperty("IsDeleted");
                    if (isDeletedProperty != null)
                    {
                        isDeletedProperty.SetValue(relatedEntity, true);
                    }

                    var deletedDateProperty = relatedEntity.GetType().GetProperty("DeletedDate");
                    if (deletedDateProperty != null)
                    {
                        deletedDateProperty.SetValue(relatedEntity, DateTime.Now);
                    }

                    _context.Update(relatedEntity);
                }
            }
        }
    }
}

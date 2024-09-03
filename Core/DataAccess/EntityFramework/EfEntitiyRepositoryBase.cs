//using Core.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Runtime.ConstrainedExecution;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.DataAccess.EntitiyFramework
//{
//    public class EfEntitiyRepositoryBase<TEntitiy,TContext> : IEntityRepository<TEntitiy>
//        where TEntitiy : class , IEntitiy,new()
//        where TContext:DbContext,new()  
//    {
//        public void Add(TEntitiy entity)
//        {
//            // IDispossable pattern implementation of c#
//            using (TContext context = new TContext())
//            {

//                var addedEntity = context.Entry(entity);
//                addedEntity.State = EntityState.Added;
//                context.SaveChanges();
//            }
//        }

//        public void Delete(TEntitiy entity)
//        {
//            using (TContext context = new TContext())
//            {
//                var deletedEntity = context.Entry(entity);
//                deletedEntity.State = EntityState.Deleted;
//                context.SaveChanges();
//            }
//        }

//        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter)
//        {
//            using (TContext context = new TContext())
//            {
//                return context.Set<TEntitiy>().SingleOrDefault(filter);
//            }
//        }

//        public List<TEntitiy> GetAll(Expression<Func<TEntitiy, bool>> filter = null)
//        {
//            using (TContext context = new TContext())
//            {
//                return filter == null ? context.Set<TEntitiy>().ToList() : context.Set<TEntitiy>().Where(filter).ToList();
//            }
//        }

//        public List<TEntitiy> GetAllByCategory(int categoryId)
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(TEntitiy entity)
//        {
//            using (TContext context = new TContext())
//            {
//                var updateddEntity = context.Entry(entity);
//                updateddEntity.State = EntityState.Modified;
//                context.SaveChanges();
//            }
//        }
//    }
//}
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntitiy, new()
        where TContext : DbContext, new()
    {
        //private readonly TContext _context;
        //private readonly DbSet<TEntity> _dbSet;

        //public EfEntityRepositoryBase(TContext context)
        //{
        //    _context = context;
        //    _dbSet = _context.Set<TEntity>();
       // }
        public async Task AddAsync(TEntity entity)
        {
            using var context = new TContext();
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            //await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            //_dbSet.Remove(entity);
            //_context.SaveChanges();
            using var context = new TContext();
        }
        public async Task AddRangeAsync(List<TEntity> entity)
        {
            //await _dbSet.AddRangeAsync(entity);
            //await _context.SaveChangesAsync();
            using var context = new TContext();
            await context.Set<TEntity>().AddRangeAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            //return await _dbSet.SingleOrDefaultAsync(filter);
            using var context = new TContext();
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {

            //return filter == null
            //    ? await _dbSet.ToListAsync()
            //    : await _dbSet.Where(filter).ToListAsync();
            using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }
        public void Update(TEntity entity)
        {
            //_dbSet.Update(entity);
            //_context.SaveChanges();
            using var context = new TContext();
            context.Set<TEntity>().Update(entity);
            context.SaveChanges();
        }
        public void UpdateList(List<TEntity> entities)
        {
            //_dbSet.UpdateRange(entities);
            //_context.SaveChanges();
            using var context = new TContext();
            context.UpdateRange(entities);
            context.SaveChanges();
        }
        public List<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            //return _dbSet.Where(expression);
            using var context = new TContext();
            return context.Set<TEntity>().Where(expression).ToList();
        }
    }
}

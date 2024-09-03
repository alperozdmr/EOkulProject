using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T :class, IEntitiy, new()
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null); //Read
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);  //Create
        Task AddRangeAsync(List<T> entity);
        void Update(T entity);   //Update
        void UpdateList(List<T> entities);
        void Delete(T entity); //Delete
        List<T> Where(Expression<Func<T, bool>> expression);
    }
}

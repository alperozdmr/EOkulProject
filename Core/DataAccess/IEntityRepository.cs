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
        List<T> GetAll(Expression<Func<T, bool>> filter = null); //Read
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);  //Create
        void Update(T entity);   //Update
        void Delete(T entity); //Delete
        List<T> GetAllByCategory(int ColorId);
    }
}

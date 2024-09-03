using Core.Utilities.Results;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Business.Abstract
{
    public interface IUserService<T> where T : class,IEntitiy,new()
    {
        Task<IResult> DeleteUser(int Id);
        IResult UpdateUser(T entity ); 
        IResult UpdateUserList(List<T> list);
        Task<IDataResult<List<T>>> GetUserByIdAsync(int id);
        List<OperationClaim> GetClaims(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task<T> GetByIdentityAsync(long Tc);
        Task<T> GetByUsername (string username);
    }
}

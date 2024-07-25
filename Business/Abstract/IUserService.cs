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
        IResult DeleteUser(int Id);
        IResult UpdateUser(T entity ); 
        IDataResult<List<T>>GetUserById(int id);
        List<OperationClaim> GetClaims(T entity);
        void Add(T entity);
        T GetByIdentity(long Tc);
        T GetByUsername (string username);
    }
}

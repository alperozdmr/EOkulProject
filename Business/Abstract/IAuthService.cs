using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService <T,Log,Reg> 
        where T : class,IEntitiy,new()
        where Log : class, IDto, new()
        where Reg : class, IDto, new()
    {
        Task<IDataResult<T>> Register(Reg RegisterDto, string password);
        Task<IDataResult<T>> Login(Log LoginDto);
        Task<IResult> UserExist(long Tc);
        Task<IDataResult<T>> ChangePassword(long Tc, string OldPassword, string NewPassword);
        IDataResult<AccessToken> CreateAccessToken(T enity);
    }
}

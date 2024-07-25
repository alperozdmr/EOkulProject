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
        IDataResult<T> Register(Reg RegisterDto, string password);
        IDataResult<T> Login(Log LoginDto);
        IResult UserExists(long Tc);
        IDataResult<T> ChangePassword(long Tc, string OldPassword, string NewPassword);
        IDataResult<AccessToken> CreateAccessToken(T enity);
    }
}

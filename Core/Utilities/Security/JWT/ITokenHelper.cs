
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper 
    {
        //Kullanıcı adı ve parolası girildikten sonra 
        //CreateToken methodu çalışcak ilgili user için
        //Database gidicek ve bu kullanıcnın claimlerini bulucak
        //Orda bir tane JasonWebToken üreticek ve onlarıda liste vericek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}

using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Serializable]

    public class Teacher :User,IEntitiy
    {
        public string UserName { get; set; }
        public long TcIdentity { get; set; }
        public int BirthYear { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

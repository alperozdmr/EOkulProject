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
    public class Student :User
    {
        public long TcIdentity { get; set; }
        public int BirthYear { get; set; }
        public int StudentClassId { get; set; }
        public StudentClass? StudentClass { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

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
    public class Student :User,IEntitiy
    {
        public override int Id { get; set; }
        public override long TcIdentity { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override string Email { get; set; }
        public override int BirthYear { get; set; }
        public override byte[] PasswordHash { get; set; }
        public override byte[] PasswordSalt { get; set; }
        public override bool IsActive { get; set; }
    }
}

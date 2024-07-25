
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    [Serializable]

    public class TeacherForRegisterDto : IDto
    {
        public long TcIdentity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }
        public string Password { get; set; }
       
    }
}

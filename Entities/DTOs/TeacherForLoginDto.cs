using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    [Serializable]

    public class TeacherForLoginDto : IDto
    {
        public string UserNanme { get; set; }
        public string Password { get; set; }
    }
}

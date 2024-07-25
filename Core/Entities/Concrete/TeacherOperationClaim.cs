using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class TeacherOperationClaim : IEntitiy
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int OperationClaimId { get; set; }
    }
}

using Core.DataAccess.EntitiyFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfTeacherDal : EfEntitiyRepositoryBase<Teacher, EOkulDb>,ITeacherDal
    {
        public List<OperationClaim> GetClaims(Teacher teacher)
        {
            using (var context = new EOkulDb())
            {
                var result = from Oc in context.OperationClaims
                             join Uc in context.TeacherOperationClaims
                                 on Oc.Id equals Uc.OperationClaimId
                             where Uc.TeacherId == teacher.Id
                             select new OperationClaim { Id = Oc.Id, Name = Oc.Name };
                return result.ToList();

            }
        }
    }
}

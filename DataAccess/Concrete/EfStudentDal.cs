using Core.DataAccess.EntityFramework;
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
    public class EfStudentDal : EfEntityRepositoryBase<Student,EOkulDb> ,IStudentDal
    {
        
        public List<OperationClaim> GetClaims(Student student)
        {
            using (var context = new EOkulDb())
            {
                var result = from Oc in context.OperationClaims
                             join Uc in context.StudentOperationClaims
                                 on Oc.Id equals Uc.OperationClaimId
                             where Uc.StudentId == student.Id
                             select new OperationClaim { Id = Oc.Id, Name = Oc.Name };
                return result.ToList();

            }
        }
    }
}

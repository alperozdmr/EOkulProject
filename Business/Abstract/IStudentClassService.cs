using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentClassService
    {
        Task<IResult> AddClass(StudentClass studentClass);
        Task<IResult> AddRangeClass(List<StudentClass> studentClass);
        IResult UpdateClass(StudentClass studentClass);
        Task<IDataResult<StudentClass>> GetClass(string ClassName);
    }
}

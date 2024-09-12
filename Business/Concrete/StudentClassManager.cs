using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentClassManager : IStudentClassService
    {
        public IStudentClassDal _studentClassDal { get; set; }
        public StudentClassManager(IStudentClassDal studentClassDal)
        {
            _studentClassDal = studentClassDal;
        }
        public async Task<IResult> AddClass(StudentClass studentClass)
        {
            await _studentClassDal.AddAsync(studentClass);
            return new SuccessResult();
        }
        public async Task<IResult> AddRangeClass(List<StudentClass> classes)
        {
            await _studentClassDal.AddRangeAsync(classes);
            return new SuccessResult();
        }

        public  IResult UpdateClass(StudentClass studentClass)
        {
            _studentClassDal.Update(studentClass);
            return new SuccessResult();
        }

        public async Task<IDataResult<StudentClass>> GetClass(string ClassName)
        {
            return new SuccessDataResult<StudentClass>(await _studentClassDal.GetAsync(x=>x.ClassName==ClassName));
        }
    }
}

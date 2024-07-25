using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentManager : IUserService<Student>
    {
        IStudentDal _studentDal;
        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }
        public void Add(Student entity)
        {
            _studentDal.Add(entity);    
        }

        public IResult DeleteUser(int Id)
        {
            Student stuent = _studentDal.Get(c => c.Id == Id);
            stuent.IsActive = false;
            _studentDal.Update(stuent);
            return new SuccessResult(Messages.UserDeleted);
        }

        public Student GetByIdentity(long Tc)
        {
            var result = _studentDal.Get(x => x.TcIdentity == Tc);
            return result;
        }

        public Student GetByUsername(string username)
        {
            return null;
        }

        public List<OperationClaim> GetClaims(Student entity)
        {
            return _studentDal.GetClaims(entity);
        }

        public IDataResult<List<Student>> GetUserById(int id)
        {
            return new SuccessDataResult<List<Student>>(_studentDal.GetAll(x => x.Id == id).Where(x => x.IsActive == true).ToList(), Messages.UserListed);


        }

        public IResult UpdateUser(Student entity)
        {
           _studentDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

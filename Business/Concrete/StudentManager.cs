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
        public async Task AddAsync(Student entity)
        {
            await _studentDal.AddAsync(entity);    
        }

        public async Task AddRangeAsync(List<Student> entity)
        {
            await _studentDal.AddRangeAsync(entity);
        }

        public async Task<IResult> DeleteUser(int Id)
        {
            Student stuent = await _studentDal.GetAsync(c => c.Id == Id);
            stuent.IsActive = false;
            _studentDal.Update(stuent);
            return new SuccessResult(Messages.UserDeleted);
        }

        public async Task<Student> GetByIdentityAsync(long Tc)
        {
            var result = await _studentDal.GetAsync(x => x.TcIdentity == Tc);
            return result;
        }

        public async Task<Student> GetByUsername(string username)
        {
            return null;
        }

        public List<OperationClaim> GetClaims(Student entity)
        {
            return _studentDal.GetClaims(entity);
        }

        public async Task<IDataResult<List<Student>>> GetUserByIdAsync(int id)
        {

            return new SuccessDataResult<List<Student>>(await _studentDal.GetAllAsync(x=>x.Id==id && x.IsActive==true), Messages.UserListed);


        }
        public IResult UpdateUser(Student entity)
        {
           _studentDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UpdateUserList(List<Student> entity)
        {
            _studentDal.UpdateList(entity);
            return new SuccessResult(Messages.UserListed);
        }
    }
}

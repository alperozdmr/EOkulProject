using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TeacherManager : IUserService<Teacher>
    {
        ITeacherDal _teacherDal;
        public TeacherManager(ITeacherDal teacherDal)
        {
            _teacherDal = teacherDal;
        }
        public void Add(Teacher entity)
        {
            _teacherDal.Add(entity);
        }

        public IResult DeleteUser(int Id)
        {
            Teacher teacher = _teacherDal.Get(c => c.Id == Id);
            teacher.IsActive = false;
            _teacherDal.Update(teacher);
            return new SuccessResult(Messages.UserDeleted);
        }

        public Teacher GetByIdentity(long Tc)
        {
            var result = _teacherDal.Get(x => x.TcIdentity == Tc);
            return result;
        }

        public Teacher GetByUsername(string username)
        {
            var result = _teacherDal.Get(x=>x.UserName == username);
            return result;
        }

        public List<OperationClaim> GetClaims(Teacher entity)
        {
            return _teacherDal.GetClaims(entity);
        }

        public IDataResult<List<Teacher>> GetUserById(int id)
        {
            return new SuccessDataResult<List<Teacher>>(_teacherDal.GetAll(x => x.Id == id).Where(x => x.IsActive == true).ToList(), Messages.UserListed);
        }

        public IResult UpdateUser(Teacher entity)
        {
            _teacherDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

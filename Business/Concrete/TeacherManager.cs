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
        public async Task AddAsync(Teacher entity)
        {
            await _teacherDal.AddAsync(entity);
        }
        public async Task AddRangeAsync(List<Teacher> entity)
        {
            await _teacherDal.AddRangeAsync(entity);
        }

        public async Task<IResult> DeleteUser(int Id)
        {
            Teacher teacher = await _teacherDal.GetAsync(c => c.Id == Id);
            teacher.IsActive = false;
            _teacherDal.Update(teacher);
            return new SuccessResult(Messages.UserDeleted);
        }

        public async Task<Teacher> GetByIdentityAsync(long Tc)
        {
            var result = await _teacherDal.GetAsync(x => x.TcIdentity == Tc);
            return result;
        }

        public async Task<Teacher> GetByUsername(string username)
        {
            var result =await _teacherDal.GetAsync(x=>x.UserName == username);
            return result;
        }

        public List<OperationClaim> GetClaims(Teacher entity)
        {
            return _teacherDal.GetClaims(entity);
        }

        public async Task<IDataResult<List<Teacher>>> GetUserByIdAsync(int id)
        {
            return new SuccessDataResult<List<Teacher>>(await _teacherDal.GetAllAsync(x => x.Id == id && x.IsActive == true), Messages.UserListed);
        }

        public IResult UpdateUser(Teacher entity)
        {
            _teacherDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UpdateUserList(List<Teacher> list)
        {
            _teacherDal.UpdateList(list);
            return new SuccessResult();
        }
    }
}

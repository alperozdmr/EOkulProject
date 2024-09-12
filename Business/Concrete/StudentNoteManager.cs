using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentNoteManager : IStudentNoteService
    {
        IStudentNoteDal _studentNoteDal;
        IUserService<Student> _userService;
        public StudentNoteManager(IStudentNoteDal studentNoteDal, IUserService<Student> userService)
        {
            _studentNoteDal = studentNoteDal;
            _userService = userService;
        }
        //[SecuredOperation("Teacher")]
        public async Task<IResult> AddToFirstTerm(AddNoteDto studentNote)
        {
            var student = await _userService.GetByIdentityAsync(studentNote.StudentId);
            var note = new StudentNote
            {
                StudentId = studentNote.StudentId,
                WhichTerm = 1,
                Math= studentNote.Math,
                Physics= studentNote.Physics,
                Biology= studentNote.Biology,   
                Chemistry= studentNote.Chemistry,   
                Turkish= studentNote.Turkish,
                History = studentNote.History,
                Geography= studentNote.Geography,
                Year = DateTime.Now.Year,
                //ClassId = student.StudentClassId
            };
           await _studentNoteDal.AddAsync(note);
            
            return new SuccessResult(); 
        }
        //[SecuredOperation("Teacher")]
        public async Task<IResult> AddToSecondTerm(AddNoteDto studentNote)
        {
            var student = await _userService.GetByIdentityAsync(studentNote.StudentId);
            var note = new StudentNote
            {
                StudentId = studentNote.StudentId,
                WhichTerm = 2,
                Math = studentNote.Math,
                Physics = studentNote.Physics,
                Biology = studentNote.Biology,
                Chemistry = studentNote.Chemistry,
                Turkish = studentNote.Turkish,
                History = studentNote.History,
                Geography = studentNote.Geography,
                Year = DateTime.Now.Year,
                //ClassId = student.StudentClassId
            };
            await _studentNoteDal.AddAsync(note);

            return new SuccessResult();
        }
        //[SecuredOperation("Teacher,Student")]
        public IDataResult<List<StudentNoteDto>> GetAllNotes()
        {
            return new SuccessDataResult<List<StudentNoteDto>>(_studentNoteDal.GetNotes()); 
        }
        //[SecuredOperation("Teacher,Student")]
        public IDataResult<List<StudentNoteDto>> GetAllNotesByName(string Name)
        {
            var results = _studentNoteDal.GetNotes().Where(x=>x.StudentName==Name).ToList();
            return new SuccessDataResult<List<StudentNoteDto>>(results);
        }

        public IDataResult<List<StudentNoteDto>> GetAllNotesByTerm(int term)
        {
            return new SuccessDataResult<List<StudentNoteDto>>(_studentNoteDal.GetNotes().Where(x=>x.WhichTerm==term).ToList());
        }

        public IDataResult<List<StudentNoteDto>> GetAllNotesByTermAndName(string name, int term)
        {
            return new SuccessDataResult<List<StudentNoteDto>>(_studentNoteDal.GetNotes().Where(x => x.WhichTerm == term && x.StudentName == name).ToList());
        }

        public IDataResult<List<StudentNote>> Where(Expression<Func<StudentNote, bool>> expression)
        {
            return new SuccessDataResult<List<StudentNote>>(_studentNoteDal.Where(expression));
        }
    }
}

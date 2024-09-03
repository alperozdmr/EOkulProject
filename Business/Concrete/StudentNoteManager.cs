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
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentNoteManager : IStudentNoteService
    {
        IStudentNoteDal _studentNoteDal;
        public StudentNoteManager(IStudentNoteDal studentNoteDal)
        {
            _studentNoteDal = studentNoteDal;
        }
        [SecuredOperation("Teacher")]
        public IResult AddNote(StudentNote studentNote)
        {
           _studentNoteDal.AddAsync(studentNote);
            return new SuccessResult(); 
        }
        [SecuredOperation("Teacher,Student")]
        public IDataResult<List<StudentNoteDto>> GetAllNotes()
        {
            return new SuccessDataResult<List<StudentNoteDto>>(_studentNoteDal.GetNotes()); 
        }
        [SecuredOperation("Teacher,Student")]
        public IDataResult<List<StudentNoteDto>> GetAllNotesByName(string Name)
        {
            var results = _studentNoteDal.GetNotes().Where(x=>x.StudentName==Name).ToList();
            return new SuccessDataResult<List<StudentNoteDto>>(results);
        }
    }
}

using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentNoteService
    {
        Task<IResult> AddToFirstTerm(AddNoteDto studentNote);
        Task<IResult> AddToSecondTerm(AddNoteDto studentNote);
        IDataResult<List<StudentNoteDto>> GetAllNotes();
        IDataResult<List<StudentNoteDto>> GetAllNotesByName(string name);
        IDataResult<List<StudentNoteDto>> GetAllNotesByTerm(int term);
        IDataResult<List<StudentNoteDto>> GetAllNotesByTermAndName(string name,int term);
        IDataResult<List<StudentNote>> Where(Expression<Func<StudentNote, bool>> expression);

    }
}

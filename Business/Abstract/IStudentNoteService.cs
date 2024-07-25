using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentNoteService
    {
        IResult AddNote(StudentNote studentNote);
        IDataResult<List<StudentNoteDto>> GetAllNotes();
        IDataResult<List<StudentNoteDto>> GetAllNotesByName(string name);
    }
}

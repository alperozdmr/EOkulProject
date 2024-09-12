using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfStudentNoteDal : EfEntityRepositoryBase<StudentNote, EOkulDb>, IStudentNoteDal
    {
        public List<StudentNoteDto> GetNotes()
        {
            using (var context = new EOkulDb())
            {
                var result = from s in context.Students
                             join Sn in context.StudentNotes
                                 on s.Id equals Sn.StudentId
                             join c in context.StudentsClasses
                             on s.StudentClassId equals c.Id
                             select new StudentNoteDto
                             {
                                 WhichTerm = Sn.WhichTerm,
                                 StudentName = s.FirstName,
                                 StudentSurname = s.LastName,
                                 StudentClass= c.ClassName,
                                 Math = Sn.Math,
                                 Physics = Sn.Physics,
                                 Biology = Sn.Biology,
                                 Chemistry = Sn.Chemistry,
                                 Turkish = Sn.Turkish,
                                 Geography = Sn.Geography,
                                 History = Sn.History,
                                 Year = Sn.Year,
                             };
                return result.ToList();

            }
        }
    }
}

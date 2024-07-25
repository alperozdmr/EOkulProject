using Core.DataAccess.EntitiyFramework;
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
    public class EfStudentNoteDal : EfEntitiyRepositoryBase<StudentNote, EOkulDb>, IStudentNoteDal
    {
        public List<StudentNoteDto> GetNotes()
        {
            using (var context = new EOkulDb())
            {
                var result = from s in context.Students
                             join Sn in context.StudentNotes
                                 on s.Id equals Sn.StudentId
                             select new StudentNoteDto
                             {
                                 StudentName = s.FirstName,
                                 StudentSurname = s.LastName,
                                 Math = Sn.Math,
                                 Physics = Sn.Physics,
                                 Biology=Sn.Biology,
                                 Chemistry=Sn.Chemistry,    
                                 Turkish=Sn.Turkish,
                                 Geography=Sn.Geography,
                                 History=Sn.History,
                             };
                return result.ToList();

            }
        }
    }
}

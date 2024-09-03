using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    [Serializable]
    public class StudentNoteDto :IDto
    {
        public int WhichTerm { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public float Math { get; set; }
        public float Physics { get; set; }
        public float Biology { get; set; }
        public float Chemistry { get; set; }
        public float Turkish { get; set; }
        public float History { get; set; }
        public float Geography { get; set; }
        public int Year { get; set; }   

    }
}

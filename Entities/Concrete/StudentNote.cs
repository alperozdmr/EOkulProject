using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Serializable]

    public class StudentNote :IEntitiy
    {
        public int Id { get; set; } = 0;
        public int StudentId { get; set; }
        public int WhichTerm { get; set; }
        public float Math { get; set; }
        public float Physics { get; set; }
        public float Biology { get; set; }
        public float Chemistry { get; set; }
        public float Turkish { get; set; }
        public float History { get; set; }
        public float Geography { get; set; }
        public int Year { get; set; } = DateTime.Now.Year; 




    }
}

using Castle.Components.DictionaryAdapter;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class StudentClass :IEntitiy
    {
        public int Id { get; set; }
        public string? ClassName { get; set; }
        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
        
        //public List<Student> Students { get; set; }
    }
}

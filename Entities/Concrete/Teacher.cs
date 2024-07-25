using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Serializable]

    public class Teacher :User,IEntitiy
    {
        public string UserName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagroceries.Models
{
    public class Test1
    {
        public Guid Test1ID { get; set; }
        public string FirstName { get; set; }
        public Test2 Test2 { get; set; }
        public Guid Test2ID { get; set; }
    }
}

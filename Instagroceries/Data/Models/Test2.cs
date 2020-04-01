using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagroceries.Models
{
    public class Test2
    {
        public Test2()
        {
            Test1s = new HashSet<Test1>();
        }
        public Guid Test2ID { get; set; }
        public string LastName { get; set; }
        public HashSet<Test1> Test1s { get; set; }
    }
}

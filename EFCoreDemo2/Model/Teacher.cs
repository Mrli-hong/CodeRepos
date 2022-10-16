using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo2.Model
{
    public class Teacher
    {
        public int ID { get; set; }
        public List<Student> Students { get; set; }
        public string Name { get; set; }
    }
}

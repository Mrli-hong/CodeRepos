using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo2
{
    public class Partment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Partment OriPartment { get; set; }
        public List<Partment> ChildrenPartment { get; set; } = new List<Partment>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependncyInjectionDemo1
{
    public class Proxy
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    public class Setting
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int[] Ids { get; set; }
        public Proxy Proxy { get; set; }
    }
}

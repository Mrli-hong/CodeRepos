using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependncyInjectionDemo1
{
    internal class TestServiceImpl
    {
        public string Name { get; set; }
        public void SayHi() {
            Console.WriteLine("Hello");
        }
    }
}

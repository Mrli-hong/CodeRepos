using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependncyInjectionDemo1
{
    internal class TestServiceImp2
    {
        private readonly TestServiceImpl test;
        public int Age { get; set; }
        public void Say() {
            test.SayHi();
        }
        public TestServiceImp2(TestServiceImpl test)
        {
            this.test = test;
        }
    }
}

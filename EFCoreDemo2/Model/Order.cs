using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo2.Model
{
    public class Order
    {
        public int ID { get; set; }
        public User Receiver { get; set; }
        public User Sender { get; set; }
        public string Content { get; set; }
    }
}

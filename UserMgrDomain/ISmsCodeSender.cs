using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgrDomain.ValueObjects;

namespace UserMgrDomain
{
    public  interface ISmsCodeSender
    {
        Task SendCodeAsync(PhoneNumber phoneNumber, string code);
    }
}

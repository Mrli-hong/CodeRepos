using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgrDomain.ServiceResult
{
    public enum CheckCodeResult
    {
        OK, PhoneNumberNotFound, LockOut, CodeError
    }
}

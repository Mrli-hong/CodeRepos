using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgrDomain.ValueObjects;

namespace UserMgrDomain.ServiceResult
{
    public record UserAccessResultEvent(PhoneNumber PhoneNumber, UserAccessResult Result) : INotification
    { }
    public enum UserAccessResult
    {
        OK, PhoneNumberNotFound, Lockout, NoPassword, PasswordError
    }
}

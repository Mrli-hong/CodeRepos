using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgrDomain.ValueObjects;

namespace UserMgrDomain.Entities
{
    public class UserLoginHistory:IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; init; }
        public PhoneNumber PhoneNumber { get; init; }
        public DateTime CreateDataTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }

        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreateDataTime = DateTime.Now;
            Message = message;
        }
    }
}

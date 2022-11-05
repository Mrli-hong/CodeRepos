using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgrDomain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public bool isLockOut;
        public DateTime? LockEnd { get; private set; }
        public int AccessFailCount { get; private set; }
        private UserAccessFail() { }
        public UserAccessFail(User user)
        {
            this.User = user;
            this.Id = user.Id;
        }
        public void Reset() 
        {
            this.AccessFailCount = 0;
            this.isLockOut = false;
            this.LockEnd = null;
        }
        public void Fail()
        {
            if (this.AccessFailCount>3)
            {
                this.LockEnd = DateTime.Now.AddMinutes(5);
                this.isLockOut = true;
            }
        }
        public bool IsLockOut()
        {
            if (this.isLockOut&&this.LockEnd>DateTime.Now)
            {
                this.Reset();
                return false;
            }
            return true;
        }

    }
}

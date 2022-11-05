using UserMgrDomain.ValueObjects;
using Zack.Commons;

namespace UserMgrDomain.Entities
{
    public class User:IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? passwordHash;//密码散列值
        public UserAccessFail UserAccessFail { get; private set; }
        private User() { }
        public User(PhoneNumber phoneNumber)
        {
            this.Id = Guid.NewGuid();
            this.PhoneNumber = phoneNumber;
            this.UserAccessFail = new UserAccessFail(this);
        }
        public bool HasPasswod()
        {
            return !string.IsNullOrEmpty(passwordHash);
        }
        public void ChangePassword(string value)
        {
            if (value.Length < 3)
                throw new ArgumentException("密码长度不能小于3");
            passwordHash = HashHelper.ComputeMd5Hash(value);
        }
        public bool CheckPassword(string password)
        {
            return passwordHash == HashHelper.ComputeMd5Hash(password);
        }
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber=phoneNumber;
        }
    }
}
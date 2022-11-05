using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgrDomain.Entities;
using UserMgrDomain.ServiceResult;
using UserMgrDomain.ValueObjects;

namespace UserMgrDomain
{
    public interface IUserDomainReposity
    {
        Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        Task<User?> FindOneAsync(Guid userId);
        Task AddNewUserLoginHistory(PhoneNumber phoneNumber, string message);
        Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code);
        Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);
        Task PublishEventAsync(UserAccessResultEvent _event);
    }
}

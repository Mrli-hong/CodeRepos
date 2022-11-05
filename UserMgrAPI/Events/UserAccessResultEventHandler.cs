using MediatR;
using UserMgrDomain;
using UserMgrDomain.ServiceResult;

namespace Users.WebAPI.Events
{
    public class UserAccessResultEventHandler
        : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserDomainReposity repository;

        public UserAccessResultEventHandler(IUserDomainReposity repository)
        {
            this.repository = repository;
        }

        public Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            var result = notification.Result;
            var phoneNum = notification.PhoneNumber;
            string msg;
            switch(result)
            {
                case UserAccessResult.OK:
                    msg = $"{phoneNum}登陆成功";
                    break;
                case UserAccessResult.PhoneNumberNotFound:
                    msg = $"{phoneNum}登陆失败，因为用户不存在";
                    break;
                case UserAccessResult.PasswordError:
                    msg = $"{phoneNum}登陆失败，密码错误";
                    break;
                case UserAccessResult.NoPassword:
                    msg = $"{phoneNum}登陆失败，没有设置密码";
                    break;
                case UserAccessResult.Lockout:
                    msg = $"{phoneNum}登陆失败，被锁定";
                    break;
                default:
                    throw new NotImplementedException();
            }
            return repository.AddNewUserLoginHistory(phoneNum,msg);
        }
    }
}

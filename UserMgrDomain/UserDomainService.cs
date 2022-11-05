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
    public class UserDomainService
    {
        private IUserDomainReposity userReposity;
        private ISmsCodeSender smsCodeSender;

        public UserDomainService(IUserDomainReposity userReposity, ISmsCodeSender smsCodeSender)
        {
            this.userReposity = userReposity;
            this.smsCodeSender = smsCodeSender;
        }
        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNum, string code)
        {
            var user = await userReposity.FindOneAsync(phoneNum);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            if (IsLockOut(user))
            {
                return CheckCodeResult.LockOut;
            }
            string? codeInServer = await userReposity.RetrievePhoneCodeAsync(phoneNum);
            if (string.IsNullOrEmpty(codeInServer))
            {
                return CheckCodeResult.CodeError;
            }
            if (code == codeInServer)
            {
                return CheckCodeResult.OK;
            }
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
        }

        public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNum,
            string password)
        {
            User? user = await userReposity.FindOneAsync(phoneNum);
            UserAccessResult result;
            if (user == null)//找不到用户
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))//用户被锁定
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.HasPasswod() == false)//没设密码
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))//密码正确
            {
                result = UserAccessResult.OK;
            }
            else//密码错误
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    this.ResetAccessFail(user);//重置
                }
                else
                {
                    this.AccessFail(user);//处理登录失败
                }
            }
            UserAccessResultEvent eventItem = new(phoneNum, result);
            await userReposity.PublishEventAsync(eventItem);
            return result;
        }
        
        public async Task<UserAccessResult> SendCodeAsync(PhoneNumber phoneNum)
        {
            var user = await userReposity.FindOneAsync(phoneNum);
            if (user == null)
            {
                return UserAccessResult.PhoneNumberNotFound;
            }
            if (IsLockOut(user))
            {
                return UserAccessResult.Lockout;
            }
            string code = Random.Shared.Next(1000, 9999).ToString();
            await userReposity.SavePhoneCodeAsync(phoneNum, code);
            await smsCodeSender.SendCodeAsync(phoneNum, code);
            return UserAccessResult.OK;
        }
        
        #region 体现User为聚合根
        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }
        public bool IsLockOut(User user)
        {
            return user.UserAccessFail.IsLockOut();
        }
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }
        #endregion
    }
}

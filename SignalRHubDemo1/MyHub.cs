using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SignalRHubDemo1
{
    [Authorize]
    public class MyHub:Hub
    {   
        private readonly UserManager<MyUser> userManager;

        public MyHub(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task SendPublicMsg(string msg)
        {
            string? Name = this.Context.User?.FindFirst(ClaimTypes.Name)?.Value;
            string connnID = this.Context.ConnectionId;
            string msgToSend = $"{connnID} {Name} {DateTime.Now}:{msg}";
            //异步方法只有一个异步操作，可以直接return不需要使用async，await
            return this.Clients.All.SendAsync("PublicMsgReceived", msgToSend);
        }

        public async Task SendPrivateMsg(string toUserName,string msg)
        {
            //接收消息用户的ID
            var user =  await userManager.FindByNameAsync(toUserName);
            //发送消息用户的Name
            string currentUserName = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
            await this.Clients.User(user.Id.ToString()).SendAsync("PrivateMsgReceived", currentUserName, msg);
            return;
        }
    }
}

using MediatR;

namespace 领域事件
{
    public record PostNotifaction(string Body):INotification;  
}

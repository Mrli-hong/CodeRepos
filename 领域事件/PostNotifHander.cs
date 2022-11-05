using MediatR;

namespace 领域事件
{
    public class PostNotifHander : INotificationHandler<PostNotifaction>
    {
        public Task Handle(PostNotifaction notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("!!!"+notification.Body);
            return Task.CompletedTask;
        }
    }
}

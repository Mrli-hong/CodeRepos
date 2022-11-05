using Zack.EventBus;

namespace ZackEvent2
{
    [EventName("sendMessage")]
    public class eventHander : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            Console.WriteLine("收到了事件名字为"+eventName+"的事件，收到的消息为："+eventData);
            return Task.CompletedTask;
        }
    }
}

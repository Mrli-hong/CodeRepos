using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.DispatchConsumersAsync = true;//设置异步消费者
using var connection = factory.CreateConnection();//创建一个连接
string exchangeName = "exchange1";//虚拟交换机的名字
string eventName = "myEvent";// routingKey的值
while (true)
{
    string msg = DateTime.Now.TimeOfDay.ToString();//待发送消息
    using var channel = connection.CreateModel();//在一个连接中创建一个虚拟的信道

    var prop =channel.CreateBasicProperties();
    prop.DeliveryMode = 2;//持久化消息、

    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");//声明一个交换机

    byte[] body = Encoding.UTF8.GetBytes(msg);
    //mandatory为true标志说明开启了消息故障检测模式，它只会让RabbitMq向你通知失败，而不会通知成功。
    channel.BasicPublish(exchange: exchangeName, routingKey: eventName,
           mandatory: true, basicProperties: prop, body: body);//发布消息        
    Console.WriteLine("发布了消息:" + msg);
    Thread.Sleep(1000);
}

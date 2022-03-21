using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQProject.Services
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }

    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "orders",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                               routingKey: "orders",
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



var factory = new ConnectionFactory() { HostName = "localhost" };
//var factory = new ConnectionFactory() { Uri = new Uri("строка_подключения_облако") };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
	channel.QueueDeclare(queue: "orders",
						 durable: false,
						 exclusive: false,
						 autoDelete: false,
						 arguments: null);

	var consumer = new EventingBasicConsumer(channel);
	consumer.Received += (model, ea) =>
	{
	Thread.Sleep(5000);
		var body = ea.Body.ToArray();
		var message = Encoding.UTF8.GetString(body);
		Console.WriteLine(" [x] Received {0}", message);
	};
	channel.BasicConsume(queue: "orders",
						 autoAck: true,
						 consumer: consumer);

	Console.WriteLine(" Press [enter] to exit.");
	Console.ReadLine();
}
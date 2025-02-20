using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Core;

namespace ConsumerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //configurando a conexão com o Rabbitmq

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                };

                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                    queue: "fila",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    //consumindo a mensagens

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, eventsargs) =>
                    {
                        var body = eventsargs.Body.ToArray();
                        var mensagem = Encoding.UTF8.GetString(body);
                        var pedido = JsonSerializer.Deserialize<Pedido>(mensagem);

                        Console.WriteLine(pedido?.ToString());
                    };

                    //despachando a mensagem
                    channel.BasicConsume(
                        queue: "fila",
                        autoAck: true,
                        consumer: consumer);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

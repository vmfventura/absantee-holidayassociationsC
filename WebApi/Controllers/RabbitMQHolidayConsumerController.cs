using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Gateway;

namespace WebApi.Controllers;

public class RabbitMQHolidayConsumerController : IRabbitMQHolidayConsumerController
{
    private IConnection _connection;
    private IModel _channel;
    private string _queueName = "holidayQueue";
    private readonly HolidayService _projectService;
    List<string> _errorMessages = new List<string>();
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMQHolidayConsumerController(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        string nameProject = "holiday_create";
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: nameProject, type: ExchangeType.Fanout);

        // _channel.QueueDeclare(queue: "holidayQueue",
        //     durable: true,
        //     exclusive: false,
        //     autoDelete: false,
        //     arguments: null);
        // _channel.QueueBind(queue: _queueName,
        //     exchange: nameProject,
        //     routingKey: "holidayKey");
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName,
            exchange: nameProject,
            routingKey: string.Empty);
        Console.WriteLine(" [*] Waiting for messages from Holiday.");
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            HolidayDTO deserializedObject = HolidayGatewayDTO.ToDTO(message);
            Console.WriteLine($" [x] Received {deserializedObject}");
            Console.WriteLine($" [x] Start checking if exists.");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<HolidayService>();

                HolidayDTO projectResultDTO = holidayService.Add(deserializedObject, _errorMessages).Result;
            }
        };
        _channel.BasicConsume(queue: _queueName,
            autoAck: true,
            consumer: consumer);
    }
}
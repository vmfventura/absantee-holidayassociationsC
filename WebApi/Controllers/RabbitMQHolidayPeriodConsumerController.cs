using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Gateway;

namespace WebApi.Controllers;

public class RabbitMQHolidayPeriodConsumerController : IRabbitMQHolidayPeriodConsumerController
{
    private IConnection _connection;
    private IModel _channel;
    private string _queueName = "holidayPeriodQueue";
    private readonly HolidayService _projectService;
    List<string> _errorMessages = new List<string>();
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMQHolidayPeriodConsumerController(IServiceScopeFactory serviceScopeFactory)
    {
        // _serviceScopeFactory = serviceScopeFactory;
        // string nameProject = "holidayPeriod_create";
        // var factory = new ConnectionFactory { HostName = "localhost" };
        // _connection = factory.CreateConnection();
        // _channel = _connection.CreateModel();
        //
        // _channel.ExchangeDeclare(exchange: nameProject, type: ExchangeType.Fanout);
        //
        // _queueName = _channel.QueueDeclare().QueueName;
        // _channel.QueueBind(queue: _queueName,
        //     exchange: nameProject,
        //     routingKey: string.Empty);
        // Console.WriteLine(" [*] Waiting for messages from Holiday.");
    }

    public void StartConsuming()
    {
        // var consumer = new EventingBasicConsumer(_channel);
        // consumer.Received += async (model, ea) =>
        // {
        //     byte[] body = ea.Body.ToArray();
        //     var message = Encoding.UTF8.GetString(body);
        //     HolidayPeriodDTO deserializedObject = HolidayPeriodGatewayDTO.ToDTO(message);
        //     Console.WriteLine($" [x] Received {deserializedObject}");
        //     Console.WriteLine($" [x] Start checking if exists.");
        //
        //     using (var scope = _serviceScopeFactory.CreateScope())
        //     {
        //         var holidayPeriodService = scope.ServiceProvider.GetRequiredService<HolidayPeriodService>();
        //
        //         HolidayPeriodDTO projectResultDTO = holidayPeriodService.Add(deserializedObject, _errorMessages).Result;
        //     }
        // };
        // _channel.BasicConsume(queue: _queueName,
        //     autoAck: true,
        //     consumer: consumer);
    }
}
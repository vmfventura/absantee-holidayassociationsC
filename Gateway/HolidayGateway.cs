using RabbitMQ.Client;
using System.Text;

namespace Gateway;

public class HolidayGateway
{
    private IConnection _connection;
    private IModel _channel;
    private string nameExchange;
    
    public HolidayGateway()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        nameExchange = "holiday_create";

        _channel.ExchangeDeclare(exchange: nameExchange, type: ExchangeType.Fanout);
    }
    public void publish(string args)
    {
        // var message = GetMessage(args);
        var body = Encoding.UTF8.GetBytes(args);
        _channel.BasicPublish(exchange: nameExchange,
            routingKey: "holidayKey",
            basicProperties: null,
            body: body);
        Console.WriteLine($" [x] Sent {args}");
    }
}
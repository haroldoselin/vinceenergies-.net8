using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace OrderService.WebApi.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IModel _channel;

        public OrdersController(IOrderService orderService)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .Build();

            _orderService = orderService;

            var factory = new ConnectionFactory
            {
                HostName = configuration["AppSettings:HostName"], //"192.168.0.43",//"192.168.15.70",
                Port = Convert.ToInt16(configuration["AppSettings:Port"]),
                UserName = configuration["AppSettings:UserName"],
                Password = configuration["AppSettings:Password"]
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare("orders", durable: false, exclusive: false, autoDelete: false);
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order));
            _channel.BasicPublish("", "orders", null, body);
            return Accepted();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}

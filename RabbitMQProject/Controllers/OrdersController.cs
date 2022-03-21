using Microsoft.AspNetCore.Mvc;
using RabbitMQProject.Models;
using RabbitMQProject.Services;

namespace RabbitMQProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;
        public OrdersController(IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            OrderDto order = new()
            {
                Id = orderDto.Id,
                ProductName = orderDto.ProductName,
                Price = orderDto.Price,
                Quantity = orderDto.Quantity
            };
            _messagePublisher.SendMessage(order);
            return Ok(new { id = order.Id });
        }
    }
}
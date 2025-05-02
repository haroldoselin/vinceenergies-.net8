using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);
    }
}

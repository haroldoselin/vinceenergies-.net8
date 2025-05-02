using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IMongoCollection<Order> _mongoCollection;

        public OrderService(AppDbContext db, IMongoDatabase mongoDb)
        {
            _db = db;
            _mongoCollection = mongoDb.GetCollection<Order>("OrderCache");
        }

        public async Task CreateOrderAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            await _mongoCollection.InsertOneAsync(order);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var cacheOrder = await _mongoCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (cacheOrder != null) return cacheOrder;

            var order = await _db.Orders.FindAsync(id);
            if (order != null)
                await _mongoCollection.InsertOneAsync(order);

            return order;
        }
    }
}

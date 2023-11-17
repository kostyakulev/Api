using Microsoft.AspNetCore.Mvc;

namespace Api.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<List<Order>> GetAllOrder();
        Task<Order?> GetSingleOrder(int id);
        Task<List<Order>> AddOrderAsync(Order order);
        Task<List<Order>?> UpdateOrderAsync(int id, Order order);
        Task<List<Order>?> DeleteOrderAsync(int id);
    }
}

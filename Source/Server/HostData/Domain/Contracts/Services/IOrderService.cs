using HostData.Domain.Contracts.Models;

namespace HostData.Domain.Contracts.Services;

public interface IOrderService : IBaseService<OrderModel>
{
    public Task<OrderModel> GetLastOrder();

    public Task<List<OrderModel>> GetOpenOrders();
}
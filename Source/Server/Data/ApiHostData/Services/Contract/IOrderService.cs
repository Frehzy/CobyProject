using ApiHostData.Domain.Models;

namespace ApiHostData.Services.Contract;

public interface IOrderService : IBaseService<OrderModel>
{
    public Task<OrderModel> GetLastOrder();

    public Task<List<OrderModel>> GetOpenOrders();
}
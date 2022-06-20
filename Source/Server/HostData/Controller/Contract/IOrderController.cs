using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface IOrderController
{
    public Task<OrderDto> CreateOrder(dynamic credentials, dynamic waiterId, dynamic tableId);

    public Task<OrderDto> RemoveOrderById(dynamic credentials, dynamic orderId);

    public Task<OrderDto> GetOrderById(dynamic orderId);

    public Task<List<OrderDto>> GetOrders();

    public Task<List<OrderDto>> GetOpenOrders();
}
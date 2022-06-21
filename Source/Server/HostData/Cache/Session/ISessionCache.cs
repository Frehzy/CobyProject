using HostData.Cache.Entities;
using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Cache.Orders;

public interface ISessionCache
{
    public IReadOnlyList<SessionAction> Sessions { get; }

    public Task<SessionDto> TryAdd(OrderModel orderModel);

    public Task<OrderModel> GetBySessionId(Guid sessionId);

    public Task Update(OrderModel orderModel);

    public Task RemoveBySessionId(Guid sessionId);

    public Task RemoveByOrderId(Guid orderId);

    public bool CheckSession(Guid credentialsId, out Guid orderId);
}
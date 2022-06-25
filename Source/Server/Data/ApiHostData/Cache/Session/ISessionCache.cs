using ApiHostData.Cache.Entities;
using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Cache.Session;

public interface ISessionCache
{
    public IReadOnlyList<SessionAction> Sessions { get; }

    public Task<SessionDto> TryAdd(OrderModel orderModel);

    public Task<(OrderModel Order, int SessionVersion)> GetBySessionId(Guid sessionId);

    public Task Update(OrderModel orderModel);

    public Task RemoveBySessionId(Guid sessionId);

    public Task RemoveByOrderId(Guid orderId);

    public bool CheckSession(Guid credentialsId, out Guid orderId);
}
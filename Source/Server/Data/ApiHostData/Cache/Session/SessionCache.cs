using ApiHostData.Cache.Entities;
using ApiHostData.Domain.Models;
using Serilog;
using Shared.Exceptions;
using Shared.Factory.Dto;
using SharedData.System.Collections.Concurrent;
using SharedData.System.Text.Json;
using System.Collections.Specialized;
using System.Text.Json;

namespace ApiHostData.Cache.Session;

public class SessionCache : ISessionCache, IDisposable
{
    private readonly ObservableConcurrentDictionary<Guid, SessionAction> _sessions = new();

    public IReadOnlyList<SessionAction> Sessions => _sessions.Values.ToList();

    public SessionCache()
    {
        _sessions.CollectionChanged += Session_CollectionChanged;
    }

    public async Task<SessionDto> TryAdd(OrderModel orderModel)
    {
        if (CheckIfExistsSession(orderModel.Id, out var existsSession) is not null)
        {
            existsSession.Reset(orderModel);
            existsSession.ResetTimer();
            return new SessionDto(existsSession.SessionId, existsSession.Version);
        }

        var session = new SessionAction(orderModel);
        session.TimerCallBackAction += RemoveSession;
        _sessions.TryAdd(session.SessionId, session);

        return new SessionDto(session.SessionId, session.Version);

        SessionAction? CheckIfExistsSession(Guid orderId, out SessionAction sessionAction) =>
            sessionAction = _sessions.FirstOrDefault(x => x.Value.Order.Id.Equals(orderId)).Value;
    }

    public async Task<(OrderModel Order, int SessionVersion)> GetBySessionId(Guid sessionId)
    {
        var session = _sessions.GetValueOrDefault(sessionId);
        return (session.Order, session.Version);
    }

    public async Task Update(OrderModel orderModel)
    {
        var order = _sessions.First(x => x.Value.Order.Id.Equals(orderModel.Id)).Value;
        order.UpdateOrder(orderModel);
    }

    public bool CheckSession(Guid sessionId, out Guid orderId)
    {
        var returnValue = _sessions.TryGetValue(sessionId, out var session);
        orderId = session.Order.Id;
        return returnValue;
    }

    public async Task RemoveByOrderId(Guid orderId)
    {
        var session = _sessions.First(x => x.Value.Order.Id.Equals(orderId));
        if (_sessions.TryRemove(session.Key, out _) is false)
            throw new EntityNotFoundException(session.Key, typeof(SessionAction).ToString());
    }

    public async Task RemoveBySessionId(Guid sessionId)
    {
        if (_sessions.TryRemove(sessionId, out _) is false)
            throw new EntityNotFoundException(sessionId, typeof(SessionAction).ToString());
    }

    public void Dispose()
    {
        foreach (var session in _sessions.Values)
            session.TimerCallBackAction -= RemoveSession;
        GC.Collect();
    }

    private void RemoveSession(SessionAction order) =>
        _sessions.TryRemove(order.SessionId, out _);

    private void Session_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (CredentialsAction newItem in e.NewItems)
                Log.Information($"{nameof(SessionCache)}. Added item: {JsonSerializer.Serialize(newItem, Options.JsonSerializerOptions)}");

        if (e.OldItems != null)
            foreach (CredentialsAction oldItem in e.OldItems)
                Log.Information($"{nameof(SessionCache)}. Remove item: {JsonSerializer.Serialize(oldItem, Options.JsonSerializerOptions)}");
    }
}
using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal static class SessionFactory
{
    public static Session Create(ISession session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.Create(x)).ToList(), session.Version);

    public static Session Create(SessionDto session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.Create(x)).ToList(), session.Version);

    public static SessionDto CreateDto(ISession session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.CreateDto(x)).ToList(), session.Version);
}
using Api.Data;
using Api.Factory.Dto;
using Api.Factory.InternalModel;

namespace Api.Factory;

internal static class SessionFactory
{
    public static Session Create(ISession session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.Create(x)).ToList(), session.Version);

    public static Session Create(SessionDto session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.Create(x)).ToList(), session.Version);

    public static SessionDto CreateDto(ISession session) =>
        new(session.OrderId, session.Orders?.Select(x => OrderFactory.CreateDto(x)).ToList(), session.Version);
}
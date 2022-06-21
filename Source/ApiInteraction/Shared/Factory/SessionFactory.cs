using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal static class SessionFactory
{
    public static Session Create(ISession session) =>
        new(session.Id, session.Version);

    public static Session Create(SessionDto session) =>
        new(session.Id, session.Version);

    public static SessionDto CreateDto(ISession session) =>
        new(session.Id, session.Version);
}
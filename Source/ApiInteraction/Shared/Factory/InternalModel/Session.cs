using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Session : ISession
{
    public Guid Id { get; set; }

    public int Version { get; set; }

    public Session(Guid id, int version)
    {
        Id = id;
        Version = version;
    }
}

using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Credentials : ICredentials
{
    public Guid Id { get; set; }

    public Credentials() { }

    public Credentials(Guid id)
    {
        Id = id;
    }
}
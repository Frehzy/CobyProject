using Shared.Data;

namespace ApiModule.Operations.Contracts;

public interface ICredentialsOperation
{
    public ICredentials CreateCredentials(string waiterPassword);

    public ISession CreateSession(IOrder order);
}
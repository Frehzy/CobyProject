using Shared.Data;

namespace Api.Operations.Contracts;

public interface ICredentialsOperation
{
    public ICredentials CreateCredentials(string waiterPassword);

    public ISession CreateSession(IOrder order);
}
using ApiModule.Http;
using ApiModule.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace ApiModule.Operations.Implementation;

internal class CredentialsOperation : ICredentialsOperation
{
    public ICredentials CreateCredentials(string waiterPassword)
    {
        var result = HttpRequest.Request<CredentialsDto>($"credentials/create/{waiterPassword}");
        return CredentialsFactory.Create(result);
    }

    public ISession CreateSession(IOrder order)
    {
        var result = HttpRequest.Request<SessionDto>($"session/create/{order.Id}");
        return SessionFactory.Create(result);
    }
}
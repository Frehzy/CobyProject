using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class CredentialsOperation : ICredentialsOperation
{
    public ICredentials CreateCredentials(string waiterPassword)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"credentials/create/{waiterPassword}");
        var result = Task.Run(async () => await HttpRequest.Get<CredentialsDto>(uri)).Result;
        return CredentialsFactory.Create(result.Content);
    }

    public ISession CreateSession(IOrder order)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"session/create/{order.Id}");
        var result = Task.Run(async () => await HttpRequest.Get<SessionDto>(uri)).Result;
        return SessionFactory.Create(result.Content);
    }
}
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
        var result = Request<CredentialsDto>($"credentials/create/{waiterPassword}");
        return CredentialsFactory.Create(result);
    }

    public ISession CreateSession(IOrder order)
    {
        var result = Request<SessionDto>($"session/create/{order.Id}");
        return SessionFactory.Create(result);
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        return result.Content;
    }
}
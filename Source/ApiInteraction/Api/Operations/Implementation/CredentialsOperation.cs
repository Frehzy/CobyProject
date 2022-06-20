using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Api.Operations.Implementation;

internal class CredentialsOperation : ICredentialsOperation
{
    private readonly IOrderOperation _orderOperation;

    public CredentialsOperation(IOrderOperation orderOperation)
    {
        _orderOperation = orderOperation;
    }

    public ICredentials CreateCredentials(string waiterPassword)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"credentials/{waiterPassword}");
        var result = Task.Run(async () => await HttpRequest.Get<CredentialsDto>(uri)).Result;
        return CredentialsFactory.Create(result.Content);
    }

    public ISession CreateSession(IOrder order)
    {
        var newOrder = _orderOperation.GetOrderById(order.Id);
        return new Session(newOrder.Id, newOrder.Version);
    }
}
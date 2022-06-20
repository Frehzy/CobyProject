using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class PaymentTypeOperation : IPaymentTypeOperation
{
    public IPaymentType CreatePaymentType(ICredentials credentials, string name, PaymentTypeKind kind, bool needOpenCashRegister)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/paymentType/create/{name}/{kind}/{needOpenCashRegister}");
        var result = Task.Run(async () => await HttpRequest.Get<PaymentTypeDto>(uri)).Result;
        return PaymentTypeFactory.Create(result.Content);
    }

    public IPaymentType GetPaymentById(Guid paymentTypeId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"paymentType/{paymentTypeId}");
        var result = Task.Run(async () => await HttpRequest.Get<PaymentTypeDto>(uri)).Result;
        return PaymentTypeFactory.Create(result.Content);
    }

    public IReadOnlyList<IPaymentType> GetPaymentTypes()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "paymentTypes");
        var result = Task.Run(async () => await HttpRequest.Get<List<PaymentTypeDto>>(uri)).Result;
        return result.Content.Select(x => PaymentTypeFactory.Create(x)).ToList();
    }

    public bool RemovePaymentType(ICredentials credentials, IPaymentType paymentType)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/paymentType/remove/{paymentType.Id}");
        var result = Task.Run(async () => await HttpRequest.Get<PaymentTypeDto>(uri)).Result;
        return result.Content is not null;
    }
}
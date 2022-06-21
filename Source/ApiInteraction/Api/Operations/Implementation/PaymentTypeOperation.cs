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
        var result = Request<PaymentTypeDto>($"{credentials.Id}/paymentType/create/{name}/{kind}/{needOpenCashRegister}");
        return PaymentTypeFactory.Create(result);
    }

    public IPaymentType GetPaymentById(Guid paymentTypeId)
    {
        var result = Request<PaymentTypeDto>($"paymentType/{paymentTypeId}");
        return PaymentTypeFactory.Create(result);
    }

    public IReadOnlyList<IPaymentType> GetPaymentTypes()
    {
        var result = Request<List<PaymentTypeDto>>($"paymentTypes");
        return result.Select(x => PaymentTypeFactory.Create(x)).ToList();
    }

    public bool RemovePaymentType(ICredentials credentials, IPaymentType paymentType)
    {
        return Request<PaymentTypeDto>($"{credentials.Id}/paymentType/remove/{paymentType.Id}") is not null;
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        return result.Content;
    }
}
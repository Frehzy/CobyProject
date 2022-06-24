using ApiModule.Http;
using ApiModule.Operations.Contracts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace ApiModule.Operations.Implementation;

internal class PaymentTypeOperation : IPaymentTypeOperation
{
    public IPaymentType CreatePaymentType(ICredentials credentials, string name, PaymentTypeKind kind, bool needOpenCashRegister)
    {
        var result = HttpRequest.Request<PaymentTypeDto>($"{credentials.Id}/paymentType/create/{name}/{kind}/{needOpenCashRegister}");
        return PaymentTypeFactory.Create(result);
    }

    public IPaymentType GetPaymentById(Guid paymentTypeId)
    {
        var result = HttpRequest.Request<PaymentTypeDto>($"paymentType/{paymentTypeId}");
        return PaymentTypeFactory.Create(result);
    }

    public IReadOnlyList<IPaymentType> GetPaymentTypes()
    {
        var result = HttpRequest.Request<List<PaymentTypeDto>>($"paymentTypes");
        return result.Select(x => PaymentTypeFactory.Create(x)).ToList();
    }

    public bool RemovePaymentType(ICredentials credentials, IPaymentType paymentType)
    {
        return HttpRequest.Request<PaymentTypeDto>($"{credentials.Id}/paymentType/remove/{paymentType.Id}") is not null;
    }
}
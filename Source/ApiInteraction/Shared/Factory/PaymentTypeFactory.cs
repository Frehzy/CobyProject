using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class PaymentTypeFactory
{
    public static PaymentType Create(IPaymentType paymentType) =>
        new(paymentType.Id, paymentType.Name, paymentType.Kind, paymentType.NeedOpenCashBox, paymentType.IsDeleted);

    public static PaymentTypeDto CreateDto(IPaymentType paymentType) =>
        new(paymentType.Id, paymentType.Name, paymentType.Kind, paymentType.NeedOpenCashBox, paymentType.IsDeleted);

    public static PaymentType Create(PaymentTypeDto paymentType) =>
        new(paymentType.Id, paymentType.Name, paymentType.Kind, paymentType.NeedOpenCashBox, paymentType.IsDeleted);
}
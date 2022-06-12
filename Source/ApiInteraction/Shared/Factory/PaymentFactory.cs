using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class PaymentFactory
{
    public static Payment Create(IPayment payment) =>
        new(payment.Id, payment.Sum, PaymentTypeFactory.Create(payment.Type), payment.Status, payment.IsDeleted);

    public static PaymentDto CreateDto(IPayment payment) =>
        new(payment.Id, payment.Sum, PaymentTypeFactory.CreateDto(payment.Type), payment.Status, payment.IsDeleted);

    public static Payment Create(PaymentDto payment) =>
        new(payment.Id, payment.Sum, PaymentTypeFactory.Create(payment.Type), payment.Status, payment.IsDeleted);
}
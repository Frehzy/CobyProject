using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Factory;

public static class PaymentFactory
{
    public static PaymentDto CreateDto(PaymentModel paymentModel) =>
        new(paymentModel.Id,
            paymentModel.Sum,
            CreateDto(paymentModel.Type),
            paymentModel.Status);

    public static PaymentTypeDto CreateDto(PaymentTypeModel paymentTypeModel) =>
        new(paymentTypeModel.Id,
            paymentTypeModel.Name,
            paymentTypeModel.Kind,
            paymentTypeModel.NeedOpenCashBox);
}
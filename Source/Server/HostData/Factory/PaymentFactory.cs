using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Factory;

public static class PaymentFactory
{
    public static PaymentDto CreateDto(PaymentModel paymentModel) =>
        new(paymentModel.Id,
            paymentModel.Sum,
            CreateDto(paymentModel.Type),
            paymentModel.Status,
            paymentModel.IsDeleted);

    public static PaymentTypeDto CreateDto(PaymentTypeModel paymentTypeModel) =>
        new(paymentTypeModel.Id,
            paymentTypeModel.Name,
            paymentTypeModel.Kind,
            paymentTypeModel.NeedOpenCashBox,
            paymentTypeModel.IsDeleted);
}
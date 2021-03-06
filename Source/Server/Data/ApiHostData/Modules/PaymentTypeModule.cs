using ApiHostData.Controller.Contract;
using Shared.Factory.Dto;
using SharedData.Modules;

namespace ApiHostData.Modules;

public class PaymentTypeModule : BaseModule
{
    private readonly IPaymentTypeController _paymentController;

    public PaymentTypeModule(IPaymentTypeController paymentController) : base()
    {
        _paymentController = paymentController;

        Get("/paymentTypes", async parameters =>
        {
            return await Execute(Context, () => _paymentController.GetPaymentTypes());
        });

        Get("/paymentType/{paymentTypeId}", async parameters =>
        {
            var paymentTypeId = parameters.paymentTypeId;
            return await Execute<PaymentTypeDto>(Context, () => _paymentController.GetPaymentTypeId(paymentTypeId));
        });

        Get("{credentialsId}/paymentType/create/{paymentTypeName}/{paymentTypeKind}/{needOpenCashBox}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var paymentTypeName = parameters.paymentTypeName;
            var paymentTypeKind = parameters.paymentTypeKind;
            var needOpenCashBox = parameters.needOpenCashBox;
            return await Execute<PaymentTypeDto>(Context, () => _paymentController.CreatePaymentType(credentialsId, paymentTypeName, paymentTypeKind, needOpenCashBox));
        });

        Get("{credentialsId}/paymentType/remove/{paymentTypeId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var paymentTypeId = parameters.paymentTypeId;
            return await Execute<PaymentTypeDto>(Context, () => _paymentController.RemovePaymentTypeById(credentialsId, paymentTypeId));
        });
    }
}
using HostData.Controller.Contract;
using Shared.Factory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostData.Modules;

public class PaymentService : BaseModule
{
    private readonly IPaymentController _paymentController;

    public PaymentService(IPaymentController paymentController) : base()
    {
        _paymentController = paymentController;

        Get("/paymentTypes", async parameters =>
        {
            return await Execute(Context, () => _paymentController.GetPaymentTypes());
        });

        Get("/paymentType/{paymentTypeId}", async parameters =>
        {
            var paymentTypeId = parameters.paymentTypeId;
            return await Execute<PaymentTypeDto>(Context, () => _paymentController.GetPaymentTypeyId(paymentTypeId));
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
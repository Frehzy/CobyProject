using ApiHostData.Controller.Contract;
using Shared.Factory.Dto;
using SharedData.Modules;

namespace ApiHostData.Modules;

public class SessionModule : BaseModule
{
    private readonly ISessionController _sessionController;

    public SessionModule(ISessionController sessionController) : base()
    {
        _sessionController = sessionController;

        Get("/{credentialsId}/{sessionId}/discount/add/{discountTypeId}/{sum}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var discountTypeId = parameters.discountTypeId;
            var sum = parameters.sum;
            return await Execute<DiscountDto>(Context, () => _sessionController.AddDiscount(credentialsId, sessionId, discountTypeId, sum));
        });

        Get("/{credentialsId}/{sessionId}/discount/remove/{discountId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var discountId = parameters.discountId;
            return await Execute<DiscountDto>(Context, () => _sessionController.RemoveDiscount(credentialsId, sessionId, discountId));
        });

        Get("/{credentialsId}/{sessionId}/product/add/{guestId}/{productItemId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var guestId = parameters.guestId;
            var productItemId = parameters.productItemId;
            return await Execute<ProductDto>(Context, () => _sessionController.AddProduct(credentialsId, sessionId, guestId, productItemId));
        });

        Get("/{credentialsId}/{sessionId}/product/remove/{productId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var productId = parameters.productId;
            return await Execute<ProductDto>(Context, () => _sessionController.RemoveProduct(credentialsId, sessionId, productId));
        });

        Get("/{credentialsId}/{sessionId}/product/addComment/{productId}/{comment}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var productId = parameters.productId;
            var comment = parameters.comment;
            return await Execute<ProductDto>(Context, () => _sessionController.AddCommentOnProduct(credentialsId, sessionId, productId, comment));
        });

        Get("/{credentialsId}/{sessionId}/product/removeComment/{productId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var productId = parameters.productId;
            return await Execute<ProductDto>(Context, () => _sessionController.RemoveCommentOnProduct(credentialsId, sessionId, productId));
        });

        Get("/{credentialsId}/{sessionId}/waiter/change/{waiterId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var waiterId = parameters.waiterId;
            return await Execute<WaiterDto>(Context, () => _sessionController.ChangeWaiter(credentialsId, sessionId, waiterId));
        });

        Get("/{credentialsId}/{sessionId}/table/change/{tableId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var tableId = parameters.tableId;
            return await Execute<TableDto>(Context, () => _sessionController.ChangeTable(credentialsId, sessionId, tableId));
        });

        Get("/{credentialsId}/{sessionId}/payment/add/{paymentTypeId}/{sum}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var paymentTypeId = parameters.paymentTypeId;
            var sum = parameters.sum;
            return await Execute<PaymentDto>(Context, () => _sessionController.AddPayment(credentialsId, sessionId, paymentTypeId, sum));
        });

        Get("/{credentialsId}/{sessionId}/payment/remove/{paymentId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            var paymentId = parameters.paymentId;
            return await Execute<PaymentDto>(Context, () => _sessionController.RemovePayment(credentialsId, sessionId, paymentId));
        });

        Get("/{credentialsId}/{sessionId}/order/close", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            return await Execute<OrderDto>(Context, () => _sessionController.CloseOrder(credentialsId, sessionId));
        });

        Get("/{credentialsId}/{sessionId}/submitChanges", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            return await Execute<OrderDto>(Context, () => _sessionController.SubmitChanges(credentialsId, sessionId));
        });

        Get("/{credentialsId}/{sessionId}/deleteOrder", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var sessionId = parameters.sessionId;
            return await Execute<OrderDto>(Context, () => _sessionController.DeleteOrder(credentialsId, sessionId));
        });
    }
}
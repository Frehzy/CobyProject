using HostData.Controller.Contract;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class SessionModule : BaseModule
{
    private readonly ISessionController _sessionController;

    public SessionModule(ISessionController sessionController) : base()
    {
        _sessionController = sessionController;

        Post("/{credentialsId}/session/discount/add/{discountTypeId}/{sum}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var discountTypeId = parameters.discountTypeId;
            var sum = parameters.sum;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.AddDiscount(session, credentialsId, discountTypeId, sum));
        });

        Post("/{credentialsId}/session/discount/remove/{discountId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var discountId = parameters.discountId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.RemoveDiscount(session, credentialsId, discountId));
        });

        Post("/{credentialsId}/session/product/add/{guestId}/{productItemId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var guestId = parameters.guestId;
            var productItemId = parameters.productItemId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.AddProduct(session, credentialsId, guestId, productItemId));
        });

        Post("/{credentialsId}/session/product/remove/{productId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var productId = parameters.productId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.RemoveProduct(session, credentialsId, productId));
        });

        Post("/{credentialsId}/session/product/addComment/{productId}/{comment}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var productId = parameters.productId;
            var comment = parameters.comment;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.AddCommentOnProduct(session, credentialsId, productId, comment));
        });

        Post("/{credentialsId}/session/product/removeComment/{productId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var productId = parameters.productId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.RemoveCommentOnProduct(session, credentialsId, productId));
        });

        Post("/{credentialsId}/session/waiter/change/{waiterId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.ChangeWaiter(session, credentialsId, waiterId));
        });

        Post("/{credentialsId}/session/table/change/{tableId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var tableId = parameters.tableId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.ChangeTable(session, credentialsId, tableId));
        });

        Post("/{credentialsId}/session/payment/add/{paymentTypeId}/{sum}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var paymentTypeId = parameters.paymentTypeId;
            var sum = parameters.sum;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.AddPayment(session, credentialsId, paymentTypeId, sum));
        });

        Post("/{credentialsId}/session/payment/remove/{paymentId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var paymentId = parameters.paymentId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.RemovePayment(session, credentialsId, paymentId));
        });

        Post("/{credentialsId}/session/order/close", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.CloseOrder(session, credentialsId));
        });

        Post("/{credentialsId}/session/submitChanges", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var session = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _sessionController.SubmitChanges(session, credentialsId));
        });
    }
}
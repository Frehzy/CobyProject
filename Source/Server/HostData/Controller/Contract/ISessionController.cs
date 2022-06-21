using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface ISessionController
{
    public Task<DiscountDto> AddDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountTypeId, dynamic sum);
    public Task<DiscountDto> RemoveDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountId);

    public Task<ProductDto> AddProduct(dynamic credentialsId, dynamic sessionId, dynamic guestId, dynamic productItemId);
    public Task<ProductDto> RemoveProduct(dynamic credentialsId, dynamic sessionId, dynamic productId);

    public Task<ProductDto> AddCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId, dynamic comment);
    public Task<ProductDto> RemoveCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId);

    public Task<WaiterDto> ChangeWaiter(dynamic credentialsId, dynamic sessionId, dynamic waiterId);

    public Task<TableDto> ChangeTable(dynamic credentialsId, dynamic sessionId, dynamic tableId);

    public Task<PaymentDto> AddPayment(dynamic credentialsId, dynamic sessionId, dynamic paymentTypeId, dynamic sum);
    public Task<PaymentDto> RemovePayment(dynamic credentialsId, dynamic sessionId, dynamic paymentId);

    public Task<OrderDto> CloseOrder(dynamic credentialsId, dynamic sessionId);

    public Task<OrderDto> SubmitChanges(dynamic credentialsId, dynamic sessionId, dynamic version);
}
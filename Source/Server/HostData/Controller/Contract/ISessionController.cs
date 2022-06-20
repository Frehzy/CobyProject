using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface ISessionController
{
    public Task<SessionDto> AddDiscount(object session, dynamic credentialsId, dynamic discountTypeId, dynamic sum);

    public Task<SessionDto> RemoveDiscount(object session, dynamic credentialsId, dynamic discountId);

    public Task<SessionDto> AddProduct(object session, dynamic credentialsId, dynamic guestId, dynamic productItemId);

    public Task<SessionDto> RemoveProduct(object session, dynamic credentialsId, dynamic productId);

    public Task<SessionDto> AddCommentOnProduct(object session, dynamic credentialsId, dynamic productId, string comment);

    public Task<SessionDto> RemoveCommentOnProduct(object session, dynamic credentialsId, dynamic productId);

    public Task<SessionDto> ChangeWaiter(object session, dynamic credentialsId, dynamic waiterId);

    public Task<SessionDto> ChangeTable(object session, dynamic credentialsId, dynamic tableId);

    public Task<SessionDto> AddPayment(object session, dynamic credentialsId, dynamic paymentTypeId, decimal sum);

    public Task<SessionDto> RemovePayment(object session, dynamic credentialsId, dynamic paymentId);

    public Task<SessionDto> CloseOrder(object session, dynamic credentialsId);

    public Task<SessionDto> SubmitChanges(object session, dynamic credentialsId);
}
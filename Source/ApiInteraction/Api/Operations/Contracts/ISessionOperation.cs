using Shared.Data;

namespace Api.Operations.Contracts;

public interface ISessionOperation
{
    public IOrder AddDiscount(ICredentials credentials, IDiscountType discountType, decimal sum);

    public IOrder RemoveDiscount(ICredentials credentials, IDiscount discount);

    public IOrder AddProduct(ICredentials credentials, IGuest guest, IProductItem productItem);

    public IOrder RemoveProduct(ICredentials credentials, IProduct product);

    public IOrder AddCommentOnProduct(ICredentials credentials, IProduct product, string comment);

    public IOrder RemoveCommentOnProduct(ICredentials credentials, IProduct product);

    public IOrder ChangeWaiter(ICredentials credentials, IWaiter waiter);

    public IOrder ChangeTable(ICredentials credentials, ITable table);

    public IOrder AddPayment(ICredentials credentials, IPaymentType paymentType, decimal sum);

    public IOrder RemovePayment(ICredentials credentials, IPayment payment);

    public IOrder CloseOrder(ICredentials credentials);

    public IOrder SubmitChanges(ICredentials credentials);
}
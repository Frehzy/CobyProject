using Shared.Data;

namespace Api.Operations.Contracts;

public interface ISessionOperation
{
    public ISession Session { get; }

    public IDiscount AddDiscount(ICredentials credentials, IDiscountType discountType, decimal sum);
    public void RemoveDiscount(ICredentials credentials, IDiscount discount);

    public IProduct AddProduct(ICredentials credentials, IGuest guest, IProductItem productItem);
    public void RemoveProduct(ICredentials credentials, IProduct product);

    public IProduct AddCommentOnProduct(ICredentials credentials, IProduct product, string comment);
    public IProduct RemoveCommentOnProduct(ICredentials credentials, IProduct product);

    public void ChangeWaiter(ICredentials credentials, IWaiter waiter);

    public void ChangeTable(ICredentials credentials, ITable table);

    public IPayment AddPayment(ICredentials credentials, IPaymentType paymentType, decimal sum);
    public void RemovePayment(ICredentials credentials, IPayment payment);

    public void CloseOrder(ICredentials credentials);

    public IOrder SubmitChanges(ICredentials credentials);
}
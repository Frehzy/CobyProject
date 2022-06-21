using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Api.Operations.Implementation;

internal class SessionOperation : ISessionOperation, IDisposable
{
    private Session _session;

    public ISession Session => _session;

    public SessionOperation(ISession session)
    {
        _session = SessionFactory.Create(session);
    }

    public IDiscount AddDiscount(ICredentials credentials, IDiscountType discountType, decimal sum)
    {
        var path = $"{credentials.Id}/{_session.Id}/discount/add/{discountType.Id}/{sum}";
        var discountDto = Request<DiscountDto>(path);
        return DiscountFactory.Create(discountDto);
    }

    public void RemoveDiscount(ICredentials credentials, IDiscount discount)
    {
        var path = $"{credentials.Id}/{_session.Id}/discount/remove/{discount.Id}";
        Request<DiscountDto>(path);
    }

    public IProduct AddProduct(ICredentials credentials, IGuest guest, IProductItem productItem)
    {
        var path = $"{credentials.Id}/{_session.Id}/product/add/{guest.Id}/{productItem.Id}";
        var productDto = Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public void RemoveProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/{_session.Id}/product/remove/{product.Id}";
        Request<ProductDto>(path);
    }

    public IProduct AddCommentOnProduct(ICredentials credentials, IProduct product, string comment)
    {
        var path = $"{credentials.Id}/{_session.Id}/product/addComment/{product.Id}/{comment}";
        var productDto = Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public IProduct RemoveCommentOnProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/{_session.Id}/product/removeComment/{product.Id}";
        var productDto = Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public void ChangeWaiter(ICredentials credentials, IWaiter waiter)
    {
        var path = $"{credentials.Id}/{_session.Id}/waiter/change/{waiter.Id}";
        Request<WaiterDto>(path);
    }

    public void ChangeTable(ICredentials credentials, ITable table)
    {
        var path = $"{credentials.Id}/{_session.Id}/table/change/{table.Id}";
        Request<TableDto>(path);
    }

    public IPayment AddPayment(ICredentials credentials, IPaymentType paymentType, decimal sum)
    {
        var path = $"{credentials.Id}/{_session.Id}/payment/add/{paymentType.Id}/{sum}";
        var paymentDto = Request<PaymentDto>(path);
        return PaymentFactory.Create(paymentDto);
    }

    public void RemovePayment(ICredentials credentials, IPayment payment)
    {
        var path = $"{credentials.Id}/{_session.Id}/payment/remove/{payment.Id}";
        Request<PaymentDto>(path);
    }

    public void CloseOrder(ICredentials credentials)
    {
        var path = $"{credentials.Id}/{_session.Id}/order/close";
        Request<OrderDto>(path);
    }

    public IOrder SubmitChanges(ICredentials credentials)
    {
        var path = $"{credentials.Id}/{_session.Id}/submitChanges/{_session.Version}";
        var orderDto = Request<OrderDto>(path);
        _session = default;
        return OrderFactory.Create(orderDto);
    }

    public void Dispose()
    {
        _session = default;
        GC.SuppressFinalize(this);
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        _session.Version++;
        return result.Content;
    }
}
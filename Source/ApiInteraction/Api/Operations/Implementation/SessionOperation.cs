using Api.Http;
using Api.Operations.Contracts;
using Api.Services.Contrancts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class SessionOperation : ISessionOperation, IDisposable
{
    private readonly IOrderService _orderService;

    public ISession Session { get; private set; }

    public SessionOperation(ISession session, IOrderService orderService)
    {
        Session = SessionFactory.Create(session);
        _orderService = orderService;
    }

    public IDiscount AddDiscount(ICredentials credentials, IDiscountType discountType, decimal sum)
    {
        var path = $"{credentials.Id}/{Session.Id}/discount/add/{discountType.Id}/{sum}";
        var discountDto = HttpRequest.Request<DiscountDto>(path);
        return DiscountFactory.Create(discountDto);
    }

    public void RemoveDiscount(ICredentials credentials, IDiscount discount)
    {
        var path = $"{credentials.Id}/{Session.Id}/discount/remove/{discount.Id}";
        HttpRequest.Request<DiscountDto>(path);
    }

    public IProduct AddProduct(ICredentials credentials, IGuest guest, IProductItem productItem)
    {
        var path = $"{credentials.Id}/{Session.Id}/product/add/{guest.Id}/{productItem.Id}";
        var productDto = HttpRequest.Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public void RemoveProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/{Session.Id}/product/remove/{product.Id}";
        HttpRequest.Request<ProductDto>(path);
    }

    public IProduct AddCommentOnProduct(ICredentials credentials, IProduct product, string comment)
    {
        var path = $"{credentials.Id}/{Session.Id}/product/addComment/{product.Id}/{comment}";
        var productDto = HttpRequest.Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public IProduct RemoveCommentOnProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/{Session.Id}/product/removeComment/{product.Id}";
        var productDto = HttpRequest.Request<ProductDto>(path);
        return ProductFactory.Create(productDto);
    }

    public void ChangeWaiter(ICredentials credentials, IWaiter waiter)
    {
        var path = $"{credentials.Id}/{Session.Id}/waiter/change/{waiter.Id}";
        HttpRequest.Request<WaiterDto>(path);
    }

    public void ChangeTable(ICredentials credentials, ITable table)
    {
        var path = $"{credentials.Id}/{Session.Id}/table/change/{table.Id}";
        HttpRequest.Request<TableDto>(path);
    }

    public IPayment AddPayment(ICredentials credentials, IPaymentType paymentType, decimal sum)
    {
        var path = $"{credentials.Id}/{Session.Id}/payment/add/{paymentType.Id}/{sum}";
        var paymentDto = HttpRequest.Request<PaymentDto>(path);
        return PaymentFactory.Create(paymentDto);
    }

    public void RemovePayment(ICredentials credentials, IPayment payment)
    {
        var path = $"{credentials.Id}/{Session.Id}/payment/remove/{payment.Id}";
        HttpRequest.Request<PaymentDto>(path);
    }

    public void CloseOrder(ICredentials credentials)
    {
        var path = $"{credentials.Id}/{Session.Id}/order/close";
        HttpRequest.Request<OrderDto>(path);
    }

    public IOrder SubmitChanges(ICredentials credentials)
    {
        var path = $"{credentials.Id}/{Session.Id}/submitChanges";
        var result = HttpRequest.Request<OrderDto>(path);
        Dispose();
        _orderService.SendOrder(result, EventType.Updated);
        return OrderFactory.Create(result);
    }

    public void DeleteOrder(ICredentials credentials)
    {
        var path = $"{credentials.Id}/{Session.Id}/deleteOrder";
        HttpRequest.Request<OrderDto>(path);
    }

    public void Dispose()
    {
        Session = default;
        GC.Collect();
    }
}
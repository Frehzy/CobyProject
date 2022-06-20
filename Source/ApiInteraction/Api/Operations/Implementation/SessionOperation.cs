using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;

namespace Api.Operations.Implementation;

internal class SessionOperation : ISessionOperation
{
    public ISession Session { get; private set; }

    public SessionOperation(ISession session)
    {
        Session = session;
    }

    public IOrder AddDiscount(ICredentials credentials, IDiscountType discountType, decimal sum)
    {
        var path = $"{credentials.Id}/session/discount/add/{discountType.Id}/{sum}";
        return Request(path);
    }

    public IOrder RemoveDiscount(ICredentials credentials, IDiscount discount)
    {
        var path = $"{credentials.Id}/session/discount/remove/{discount.Id}";
        return Request(path);
    }

    public IOrder AddProduct(ICredentials credentials, IGuest guest, IProductItem productItem)
    {
        var path = $"{credentials.Id}/session/product/add/{guest.Id}/{productItem.Id}";
        return Request(path);
    }

    public IOrder RemoveProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/session/product/remove/{product.Id}";
        return Request(path);
    }

    public IOrder AddCommentOnProduct(ICredentials credentials, IProduct product, string comment)
    {
        var path = $"{credentials.Id}/session/product/addComment/{product.Id}/{comment}";
        return Request(path);
    }

    public IOrder RemoveCommentOnProduct(ICredentials credentials, IProduct product)
    {
        var path = $"{credentials.Id}/session/product/removeComment/{product.Id}";
        return Request(path);
    }

    public IOrder ChangeWaiter(ICredentials credentials, IWaiter waiter)
    {
        var path = $"{credentials.Id}/session/waiter/change/{waiter.Id}";
        return Request(path);
    }

    public IOrder ChangeTable(ICredentials credentials, ITable table)
    {
        var path = $"{credentials.Id}/session/table/change/{table.Id}";
        return Request(path);
    }

    public IOrder AddPayment(ICredentials credentials, IPaymentType paymentType, decimal sum)
    {
        var path = $"{credentials.Id}/session/payment/add/{paymentType.Id}/{sum}";
        return Request(path);
    }

    public IOrder RemovePayment(ICredentials credentials, IPayment payment)
    {
        var path = $"{credentials.Id}/session/payment/remove/{payment.Id}";
        return Request(path);
    }

    public IOrder CloseOrder(ICredentials credentials)
    {
        var path = $"{credentials.Id}/session/order/close";
        return Request(path);
    }

    public IOrder SubmitChanges(ICredentials credentials)
    {
        var path = $"{credentials.Id}/session/submitChanges";
        return Request(path);
    }

    private IOrder Request(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress(); 
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var sessionDto = SessionFactory.CreateDto(Session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        Session = SessionFactory.Create(result.Content);
        return Session.Orders.OrderByDescending(x => x.Version).First();
    }
}
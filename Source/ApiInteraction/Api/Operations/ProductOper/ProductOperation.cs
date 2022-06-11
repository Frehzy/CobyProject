using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.ProductOper;

internal class ProductOperation : IProductOperation
{
    public IReadOnlyList<IProduct> AddProduct(IOrder order, IWaiter waiter, IProduct product, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/product/add/{waiter.Id}/{product.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetProducts()).ToList();
    }

    public IReadOnlyList<IProduct> GetProducts()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "products");
        var result = HttpRequest.Get<List<ProductDto>>(uri);
        return result.Content.Select(x => ProductFactory.Create(x)).ToList();
    }

    public IReadOnlyList<IProduct> RemoveProduct(IOrder order, IProduct product, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{order.Id}/product/remove/{product.Id}");
        var result = HttpRequest.Post(uri, SessionFactory.CreateDto(session));
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetProducts()).ToList();
    }
}
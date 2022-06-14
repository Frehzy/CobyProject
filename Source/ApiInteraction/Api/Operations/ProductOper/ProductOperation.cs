using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.ProductOper;

internal class ProductOperation : IProductOperation
{
    public IReadOnlyList<IProduct> AddProduct(ICredentials credentials, IProduct product, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/product/add/{credentials.Id}/{product.Id}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetProducts()).ToList();
    }

    public IReadOnlyList<IProduct> GetProducts()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "products");
        var result = Task.Run(async () => await HttpRequest.Get<List<ProductDto>>(uri)).Result;
        return result.Content.Select(x => ProductFactory.Create(x)).ToList();
    }

    public IReadOnlyList<IProduct> RemoveProduct(ICredentials credentials, IProduct product, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/product/remove/{credentials.Id}/{product.Id}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetProducts()).ToList();
    }
}
using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class ProductItemOperation : IProductItemOperation
{
    public IProductItem CreateProduct(ICredentials credentials, string name, decimal price, ProductType productType)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/product/create/{name}/{price}/{productType}");
        var result = Task.Run(async () => await HttpRequest.Get<ProductItemDto>(uri)).Result;
        return ProductItemFactory.Create(result.Content);
    }

    public IProductItem GetProductById(Guid productId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"product/{productId}");
        var result = Task.Run(async () => await HttpRequest.Get<ProductItemDto>(uri)).Result;
        return ProductItemFactory.Create(result.Content);
    }

    public IReadOnlyList<IProductItem> GetProducts()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "products");
        var result = Task.Run(async () => await HttpRequest.Get<List<ProductItemDto>>(uri)).Result;
        return result.Content.Select(x => ProductItemFactory.Create(x)).ToList();
    }

    public bool RemoveProduct(ICredentials credentials, IProductItem productItem)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/product/remmove/{productItem.Id}");
        var result = Task.Run(async () => await HttpRequest.Get<ProductItemDto>(uri)).Result;
        return result.Content is not null;
    }
    /*public IReadOnlyList<IProduct> AddProduct(ICredentials credentials, IProduct product, ref ISession session)
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
    }*/
}
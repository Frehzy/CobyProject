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
}
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
        var result = Request<ProductItemDto>($"{credentials.Id}/product/create/{name}/{price}/{productType}");
        return ProductItemFactory.Create(result);
    }

    public IProductItem GetProductById(Guid productId)
    {
        var result = Request<ProductItemDto>($"product/{productId}");
        return ProductItemFactory.Create(result);
    }

    public IReadOnlyList<IProductItem> GetProducts()
    {
        var result = Request<List<ProductItemDto>>($"products");
        return result.Select(x => ProductItemFactory.Create(x)).ToList();
    }

    public bool RemoveProduct(ICredentials credentials, IProductItem productItem)
    {
        return Request<ProductItemDto>($"{credentials.Id}/product/remmove/{productItem.Id}") is not null;
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        return result.Content;
    }
}
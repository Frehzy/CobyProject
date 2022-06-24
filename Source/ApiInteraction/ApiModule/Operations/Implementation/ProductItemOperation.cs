using ApiModule.Http;
using ApiModule.Operations.Contracts;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace ApiModule.Operations.Implementation;

internal class ProductItemOperation : IProductItemOperation
{
    public IProductItem CreateProduct(ICredentials credentials, string name, decimal price, ProductType productType)
    {
        var result = HttpRequest.Request<ProductItemDto>($"{credentials.Id}/product/create/{name}/{price}/{productType}");
        return ProductItemFactory.Create(result);
    }

    public IProductItem GetProductById(Guid productId)
    {
        var result = HttpRequest.Request<ProductItemDto>($"product/{productId}");
        return ProductItemFactory.Create(result);
    }

    public IReadOnlyList<IProductItem> GetProducts()
    {
        var result = HttpRequest.Request<List<ProductItemDto>>($"products");
        return result.Select(x => ProductItemFactory.Create(x)).ToList();
    }

    public bool RemoveProduct(ICredentials credentials, IProductItem productItem)
    {
        return HttpRequest.Request<ProductItemDto>($"{credentials.Id}/product/remmove/{productItem.Id}") is not null;
    }
}
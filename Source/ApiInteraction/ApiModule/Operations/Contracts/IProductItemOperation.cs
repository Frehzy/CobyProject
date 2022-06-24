using Shared.Data;
using Shared.Data.Enum;

namespace ApiModule.Operations.Contracts;

public interface IProductItemOperation
{
    public IReadOnlyList<IProductItem> GetProducts();

    public IProductItem GetProductById(Guid productId);

    public IProductItem CreateProduct(ICredentials credentials, string name, decimal price, ProductType productType);

    public bool RemoveProduct(ICredentials credentials, IProductItem productItem);
}
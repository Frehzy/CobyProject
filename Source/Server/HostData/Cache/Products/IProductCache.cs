using Shared.Data;

namespace HostData.Cache.Products;

public interface IProductCache
{
    IReadOnlyCollection<IProduct> Products { get; }

    IReadOnlyCollection<IProduct> Goods { get; }

    IReadOnlyCollection<IProduct> Dishes { get; }

    IReadOnlyCollection<IProduct> Modifier { get; }

    IProduct GetProductById(Guid productId);

    void AddOrUpdate(IProduct product);

    IProduct RemoveProduct(Guid productId);
}

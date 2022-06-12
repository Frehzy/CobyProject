using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache.Products;

internal class ProductCache : IProductCache
{
    private readonly ConcurrentDictionary<Guid, IProduct> _productsCache = new();

    public IReadOnlyCollection<IProduct> Goods => _productsCache.Values.Where(x => x.Type.HasFlag(ProductType.Goods)).ToList();

    public IReadOnlyCollection<IProduct> Dishes => _productsCache.Values.Where(x => x.Type.HasFlag(ProductType.Dish)).ToList();

    public IReadOnlyCollection<IProduct> Modifier => _productsCache.Values.Where(x => x.Type.HasFlag(ProductType.Modifier)).ToList();

    public IReadOnlyCollection<IProduct> Products => _productsCache.Values.ToList();

    public void AddOrUpdate(IProduct product)
    {
        if (_productsCache.TryGetValue(product.Id, out var productOnCache) is false)
            _productsCache.TryAdd(product.Id, product);
        else
            _productsCache.TryUpdate(product.Id, product, productOnCache);
    }

    public IProduct GetProductById(Guid productId)
    {
        if (_productsCache.TryGetValue(productId, out var returnProduct) is false)
            throw new EntityNotFoundException(productId, nameof(IProduct));
        return returnProduct;
    }

    public IProduct RemoveProduct(Guid productId)
    {
        var product = GetProductById(productId);

        if (product.IsDeleted is true)
            throw new CantRemoveDeletedItemException(product.Id);

        var productDto = ProductFactory.CreateDto(product);
        productDto = productDto with { Status = ProductStatus.Deleted, IsDeleted = true };

        AddOrUpdate(ProductFactory.Create(productDto));
        return GetProductById(productId);
    }
}
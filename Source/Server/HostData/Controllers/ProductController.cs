using HostData.Cache.Orders;
using HostData.Cache.Products;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class ProductController
{
    private readonly IOrderCache _orderCache;
    private readonly IProductCache _productCache;

    public ProductController(IOrderCache orderCache, IProductCache productCache)
    {
        _orderCache = orderCache;
        _productCache = productCache;
    }

    public async Task<SessionDto> AddProduct(dynamic orderId, dynamic productId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (Guid.TryParse(productId.ToString(), out Guid pId) is false)
                throw new ArgumentException($"{nameof(productId)} must be type Guid", nameof(productId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var product = _productCache.GetProductById(pId);
            if (product.Type.HasFlag(ProductType.Goods) is false)
                throw new CantAddProductException(product);

            var productDto = ProductFactory.CreateDto(product);

            var productsList = session.Orders.Count <= 0
                ? order.GetProducts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetProducts();

            productsList.Add(productDto);
            var newOrder = order with { Products = productsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    public async Task<SessionDto> RemoveProduct(dynamic orderId, dynamic productId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            if (Guid.TryParse(orderId.ToString(), out Guid oId) is false)
                throw new ArgumentException($"{nameof(orderId)} must be type Guid", nameof(orderId));

            if (Guid.TryParse(productId.ToString(), out Guid pId) is false)
                throw new ArgumentException($"{nameof(productId)} must be type Guid", nameof(productId));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var productsList = session.Orders.Count <= 0
                ? order.GetProducts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetProducts();

            var product = productsList.First(x => x.Id.Equals(pId));
            product = product with { IsDeleted = true };

            var newOrder = order with { Products = productsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        return await Task.Run(() =>
        {
            return _productCache.Products.Select(x => ProductFactory.CreateDto(x)).ToList();
        });
    }
}
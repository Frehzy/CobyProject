using HostData.Cache.Orders;
using HostData.Cache.Products;
using HostData.Cache.Waiters;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class ProductController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IProductCache _productCache;

    public ProductController(IOrderCache orderCache, IProductCache productCache, IWaiterCache waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _productCache = productCache;
    }

    public async Task<SessionDto> AddProduct(dynamic orderId, dynamic credentialsId, dynamic productId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var pId = CheckDynamicGuid(productId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            var waiter = CheckCredentials(cId, EmployeePermission.CanAddDishesOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));

            IProduct product = _productCache.GetProductById(pId);
            if (product.Type.HasFlag(ProductType.Goods) is false)
                throw new CantAddProductException(product);

            var productDto = ProductFactory.CreateDto(product) with { WaiterId = waiter.Id };

            var productsList = session.Orders.Count <= 0
                ? order.GetProducts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetProducts();

            productsList.Add(productDto);
            var newOrder = order with { Products = productsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    public async Task<SessionDto> RemoveProduct(dynamic orderId, dynamic credentialsId, dynamic productId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var pId = CheckDynamicGuid(productId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(orderId));

            var productsList = session.Orders.Count <= 0
                ? order.GetProducts()
                : session.Orders.OrderByDescending(x => x.Version).First().GetProducts();

            var product = productsList.First(x => x.Id.Equals(pId));
            if (product.PrintTime is not null)
                CheckCredentials(cId, EmployeePermission.CanRemoveDishesOnOrder);
            else
                CheckCredentials(cId, EmployeePermission.CanRemovePrintedDishesOnOrder);

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
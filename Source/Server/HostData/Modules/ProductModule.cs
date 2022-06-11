using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Cache.Products;
using HostData.Controllers;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class ProductModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly ProductController _productController;

    public ProductModule(IOrderCache orderCache, IProductCache productCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _productController = new(orderCache, productCache);

        Post("/{orderId}/product/add/{productId}", parameters =>
        {
            var orderId = parameters.orderId;
            var productId = parameters.productId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _productController.AddProduct(orderId, productId, obj));
        });

        Post("/{orderId}/product/remove/{productId}", parameters =>
        {
            var orderId = parameters.orderId;
            var productId = parameters.productId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _productController.RemoveProduct(orderId, productId, obj));
        });

        Get("/products", parameters =>
        {
            return Execute(Context, () => _productController.GetProducts());
        });
    }
}
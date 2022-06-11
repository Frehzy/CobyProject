using HostData.Cache.Config;
using HostData.Cache.Discounts;
using HostData.Cache.Orders;
using HostData.Controllers;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class DiscountModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly DiscountController _discountController;

    public DiscountModule(IOrderCache orderCache, IDiscountCache discountCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _discountController = new(orderCache, discountCache);

        Post("/{orderId}/discount/add/{discountId}", parameters =>
        {
            var orderId = parameters.orderId;
            var discountId = parameters.discountId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _discountController.AddDiscount(orderId, discountId, obj));
        });

        Post("/{orderId}/discount/remove/{discountId}", parameters =>
        {
            var orderId = parameters.orderId;
            var discountId = parameters.discountId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _discountController.RemoveDiscount(orderId, discountId, obj));
        });

        Get("/discounts", parameters =>
        {
            return Execute(Context, () => _discountController.GetDiscounts());
        });
    }
}
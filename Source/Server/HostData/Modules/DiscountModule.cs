using HostData.Cache.Config;
using HostData.Cache.Discounts;
using HostData.Cache.Orders;
using HostData.Cache.Waiters;
using HostData.Controllers;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class DiscountModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly DiscountController _discountController;

    public DiscountModule(IOrderCache orderCache, IDiscountCache discountCache, IWaiterCache waiterCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _discountController = new(orderCache, discountCache, waiterCache);

        Post("/{orderId}/discount/add/{credentialsId}/{discountId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var discountId = parameters.discountId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _discountController.AddDiscount(orderId, credentialsId, discountId, obj));
        });

        Post("/{orderId}/discount/remove/{credentialsId}/{discountId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var discountId = parameters.discountId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _discountController.RemoveDiscount(orderId, credentialsId, discountId, obj));
        });

        Get("/discounts", parameters =>
        {
            return Execute(Context, () => _discountController.GetDiscounts());
        });
    }
}
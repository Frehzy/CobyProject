using HostData.Domain.Contracts.Services;
using HostData.Services;

namespace ASPHost.ConfigureServices;

public static class ConfigureServiceBase
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<IDiscountTypeService, DiscountTypeService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPaymentTypeService, PaymentTypeService>();
        services.AddTransient<IProductItemService, ProductItemService>();
        services.AddTransient<ITableService, TableService>();
        services.AddTransient<IWaiterService, WaiterService>();
    }
}
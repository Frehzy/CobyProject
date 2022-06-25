using ApiHostData.Controller.Contract;
using ApiHostData.Controller.Implementation;

namespace ApiHostServices.ConfigureServices;

public static class ConfigureController
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<IWaiterController, WaiterController>();
        services.AddTransient<ICredentialsController, CredentialsController>();
        services.AddTransient<ITableController, TableController>();
        services.AddTransient<IPaymentTypeController, PaymentTypeController>();
        services.AddTransient<IDiscountTypeController, DiscountTypeController>();
        services.AddTransient<IProductItemController, ProductItemController>();
        services.AddTransient<IOrderController, OrderController>();
        services.AddTransient<ISessionController, SessionController>();
    }
}
using Api.Operations.Contracts;
using Api.Operations.Implementation;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Notification;

internal static class Configure
{
    public static IServiceProvider ConfigureServices() =>
        new ServiceCollection()
            .AddSingleton<ICredentialsOperation, CredentialsOperation>()
            .AddSingleton<IDiscountTypeOperation, DiscountTypeOperation>()
            .AddSingleton<IOrderOperation, OrderOperation>()
            .AddSingleton<IPaymentTypeOperation, PaymentTypeOperation>()
            .AddSingleton<IProductItemOperation, ProductItemOperation>()
            .AddSingleton<ISessionOperation, SessionOperation>()
            .AddSingleton<ITableOperation, TableOperation>()
            .AddSingleton<IWaiterOperation, WaiterOperation>()
            .AddSingleton<INotificationService, NotificationService>()
            .AddSingleton<IOrderService, OrderService>()
            .BuildServiceProvider();
}
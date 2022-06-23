using Api.Operations.Contracts;
using Api.Operations.Implementation;
using Api.Services.Contrancts;
using Api.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;

namespace Api.Notification;

internal static class Configure
{
    public static IServiceProvider ConfigureServices()
    {
        var ip = NetOperation.GetLocalIPAddress();
        var url = new Uri(string.Format("http://{0}:{1}/", ip.ToString(), 5050));

        return new ServiceCollection()
            .AddSingleton<ICredentialsOperation, CredentialsOperation>()
            .AddSingleton<IDiscountTypeOperation, DiscountTypeOperation>()
            .AddSingleton<IOrderOperation, OrderOperation>()
            .AddSingleton<IPaymentTypeOperation, PaymentTypeOperation>()
            .AddSingleton<IProductItemOperation, ProductItemOperation>()
            .AddSingleton<ISessionOperation, SessionOperation>()
            .AddSingleton<ITableOperation, TableOperation>()
            .AddSingleton<IWaiterOperation, WaiterOperation>()
            .AddSingleton<INotificationService, NotificationService>()
            .AddSingleton<IOrderService, OrderService>(_ => new OrderService(url))
            .AddSingleton<IWaiterService, WaiterService>(_ => new WaiterService(url))
            .BuildServiceProvider();
    }
}
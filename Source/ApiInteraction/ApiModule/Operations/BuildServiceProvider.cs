using ApiModule.Api;
using ApiModule.Operations.Contracts;
using ApiModule.Operations.Implementation;
using ApiModule.Services.Contrancts;
using ApiModule.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;

namespace ApiModule.Operations;

internal static class BuildServiceProvider
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
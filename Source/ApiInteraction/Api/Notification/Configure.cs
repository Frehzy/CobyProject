using Api.Http;
using Api.Operations.Contracts;
using Api.Operations.Implementation;
using Api.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;

namespace Api.Notification;

public static class Configure
{
    public static IServiceProvider ConfigureServices()
    {
        var ip = NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "ordersNotification");

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
            .AddTransient<IOrderService, OrderService>(_ => new OrderService(
                 new HubConnectionBuilder().WithUrl(uri)
                     .Build()))
            .BuildServiceProvider();
    }
}
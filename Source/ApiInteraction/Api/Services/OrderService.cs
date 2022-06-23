using Api.Http;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Configuration;
using Shared.Factory.Dto;
using System.Net;

namespace Api.Services;

internal class OrderService : IOrderService
{
    private const string ORDER_NOTIFICATION = "ordersNotification";

    private readonly HubConnection _connection;
    private readonly IPAddress _ip;
    private readonly Uri _orderNotificationUrl;

    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    public event Action<OrderDto> OnOrder;

    public OrderService()
    {
        _ip ??= NetOperation.GetLocalIPAddress();
        _orderNotificationUrl ??= HttpUtility.CreateUri(_ip.ToString(), 5050, ORDER_NOTIFICATION);

        _connection ??= new HubConnectionBuilder().WithUrl(_orderNotificationUrl).Build();
        _connection.On<OrderDto>(nameof(OnOrder), (order) => OnOrder?.Invoke(order));
    }

    public async Task Connect() =>
        await _connection.StartAsync();

    public async Task Disconnect() =>
        await _connection.StopAsync();

    public async Task SendOrder(OrderDto order) =>
        await _connection.InvokeAsync(nameof(SendOrder), order);
}
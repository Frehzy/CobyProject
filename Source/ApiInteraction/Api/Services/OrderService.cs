using Microsoft.AspNetCore.SignalR.Client;
using Shared.Factory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services;

internal class OrderService : IOrderService
{
    private readonly HubConnection _connection;
    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    public event Action<OrderDto>? OnOrder;

    public OrderService(HubConnection connection)
    {
        _connection = connection;
        _connection.On<OrderDto>(nameof(OnOrder), (order) => OnOrder?.Invoke(order));
    }

    public async Task Connect() => 
        await _connection.StartAsync();

    public async Task Disconnect() => 
        await _connection.StopAsync();

    public async Task SendOrder(OrderDto order) => 
        await _connection.InvokeAsync(nameof(SendOrder), order);
}
﻿using ApiHostData.Cache.Licence;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Microsoft.AspNetCore.SignalR;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiHostData.Hubs;

public class OrderHub : BaseHub
{
    private readonly IOrderService _orderService;

    public OrderHub(IOrderService orderService, ILicenceCache licenceCache) : base(licenceCache)
    {
        _orderService = orderService;
    }

    public override async Task OnConnectedAsync()
    {
        if (await base.CheckLicence() is false)
            return;
        else
        {
            foreach (var order in await _orderService.Get())
                await Clients.Client(Context.ConnectionId).SendAsync("OnOrder", OrderFactory.CreateDto(order), EventType.Updated);

            await base.OnConnectedAsync();
        }
    }

    public async Task SendOrder(OrderDto order, EventType eventType)
    {
        await Clients.All.SendAsync("OnOrder", order, eventType);
    }
}
﻿using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Cache.Tables;
using HostData.Cache.Waiters;
using HostData.Controllers;
using Nancy;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class OrderModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, ITableCache tableCache, IWaiterCache waiterCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _orderController = new(orderCache, tableCache, waiterCache);

        Get("/orders/{orderId}", parameters =>
        {
            var orderId = parameters.orderId;
            return Execute<OrderDto>(Context, () => _orderController.GetOrderById(orderId));
        });

        Get("/allOrders", parameters =>
        {
            return Execute(Context, () => _orderController.GetOrders());
        });

        Get("/openOrders", parameters =>
        {
            return Execute(Context, () => _orderController.GetOpenOrders());
        });

        Get("/order/create/{credentialsId}/{waiterId}/{tablesId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            IEnumerable<dynamic> tablesId = parameters.tablesId.Split('/');
            return Execute<OrderDto>(Context, () => _orderController.CreateOrder(credentialsId, waiterId, tablesId));
        });

        Post("/order/remove/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<OrderDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.RemoveOrderById(credentialsId, obj.Id));
        });

        Post("/order/submitChanges/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.SubmitChanges(credentialsId, obj));
        });

        Post("/order/closeOrder/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.CloseOrder(credentialsId, obj));
        });
    }
}

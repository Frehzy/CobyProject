using ApiHostData.Controller.Contract;
using Shared.Factory.Dto;
using SharedData.Modules;

namespace ApiHostData.Modules;

public class OrderModule : BaseModule
{
    private readonly IOrderController _orderController;

    public OrderModule(IOrderController orderController) : base()
    {
        _orderController = orderController;

        Get("/orders", async parameters =>
        {
            return await Execute(Context, () => _orderController.GetOrders());
        });

        Get("/openOrders", async parameters =>
        {
            return await Execute(Context, () => _orderController.GetOpenOrders());
        });

        Get("/order/{orderId}", async parameters =>
        {
            var orderId = parameters.orderId;
            return await Execute<OrderDto>(Context, () => _orderController.GetOrderById(orderId));
        });

        Get("{credentialsId}/order/create/{waiterId}/{tableId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var tableId = parameters.tableId;
            return await Execute<OrderDto>(Context, () => _orderController.CreateOrder(credentialsId, waiterId, tableId));
        });

        Get("{credentialsId}/order/remove/{orderId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var orderId = parameters.orderId;
            return await Execute<OrderDto>(Context, () => _orderController.RemoveOrderById(credentialsId, orderId));
        });
    }
}
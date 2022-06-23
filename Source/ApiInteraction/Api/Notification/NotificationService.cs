using Api.Services.Contrancts;
using Shared.Data;
using Shared.Factory;

namespace Api.Notification;

internal class NotificationService : INotificationService
{
    public event Action<IOrder>? ReceiveOrder;

    public event Action<IWaiter>? ReceiveWaiter;

    public NotificationService(IOrderService orderService, IWaiterService waiterService)
    {
        orderService.ReceiveEvent += (order) => ReceiveOrder?.Invoke(OrderFactory.Create(order));

        waiterService.ReceiveEvent += (waiter) => ReceiveWaiter?.Invoke(WaiterFactory.Create(waiter));
    }
}
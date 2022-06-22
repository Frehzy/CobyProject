using Api.Services;
using Shared.Data;
using Shared.Factory;

namespace Api.Notification;

internal class NotificationService : INotificationService
{
    public event Action<IOrder>? OnOrder;

    public NotificationService(IOrderService orderService)
    {
        orderService.OnOrder += (order) =>
        {
            OnOrder?.Invoke(OrderFactory.Create(order));
        };
    }
}
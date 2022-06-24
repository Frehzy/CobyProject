using Api.Services.Contrancts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.InternalModel;

namespace Api.Notification;

internal class NotificationService : INotificationService
{
    public event Action<IEntityChangedEvent<IOrder>>? ReceiveOrder;

    public event Action<IEntityChangedEvent<IWaiter>>? ReceiveWaiter;

    public NotificationService(IOrderService orderService, IWaiterService waiterService)
    {
        orderService.ReceiveEvent += (entityEvent) => ReceiveOrder?.Invoke(new EntityChangedEvent<IOrder>(OrderFactory.Create(entityEvent.Entity), entityEvent.EventType));

        waiterService.ReceiveEvent += (entityEvent) => ReceiveWaiter?.Invoke(new EntityChangedEvent<IWaiter>(WaiterFactory.Create(entityEvent.Entity), entityEvent.EventType));
    }
}
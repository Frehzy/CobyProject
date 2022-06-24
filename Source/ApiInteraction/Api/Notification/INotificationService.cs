using Shared.Data;

namespace Api.Notification;

public interface INotificationService
{
    public event Action<IEntityChangedEvent<IOrder>>? ReceiveOrder;

    public event Action<IEntityChangedEvent<IWaiter>>? ReceiveWaiter;
}
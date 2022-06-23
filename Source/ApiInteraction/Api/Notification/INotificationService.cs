using Shared.Data;

namespace Api.Notification;

public interface INotificationService
{
    public event Action<IOrder>? ReceiveOrder;

    public event Action<IWaiter>? ReceiveWaiter;
}
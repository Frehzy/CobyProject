using Shared.Data;

namespace Api.Notification;

public interface INotificationService
{
    public event Action<IOrder>? OnOrder;
}
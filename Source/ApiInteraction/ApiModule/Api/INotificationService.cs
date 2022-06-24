using Shared.Data;

namespace ApiModule.Api;

public interface INotificationService
{
    public event Action<IEntityChangedEvent<IOrder>>? ReceiveOrder;

    public event Action<IEntityChangedEvent<IWaiter>>? ReceiveWaiter;
}
using HostData.Domain.Contracts.Models;

namespace HostData.Cache.Entities;

public class CredentialsAction
{
    private readonly Timer _timeoutTimer;

    public event Action<CredentialsAction> Timeout;

    public Guid CredentialsId { get; private set; }

    public WaiterPermissionModel Waiter { get; private set; }

    public CredentialsAction(Guid credentialsId, WaiterPermissionModel waiter)
    {
        CredentialsId = credentialsId;
        Waiter = waiter;
        _timeoutTimer = new Timer(TimeoutHandler, default, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
    }

    public void OnDataRecieved() =>
        _timeoutTimer.Change(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

    private void TimeoutHandler(object data) =>
        Timeout?.Invoke(this);
}
using ApiHostData.Domain.Models;

namespace ApiHostData.Cache.Entities;

public class CredentialsAction
{
    private readonly Timer _timeoutTimer;

    public event Action<CredentialsAction> TimerCallBackAction;

    public Guid CredentialsId { get; private set; }

    public WaiterModel Waiter { get; private set; }

    public CredentialsAction(WaiterModel waiter)
    {
        Waiter = waiter ?? throw new ArgumentNullException(nameof(waiter));
        CredentialsId = waiter.Password is "ADMINPASSWORD"
            ? Guid.Empty
            : Guid.NewGuid();
        _timeoutTimer = waiter.Password is "ADMINPASSWORD"
            ? new Timer(TimeoutHandler, this, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan)
            : new Timer(TimeoutHandler, this, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
    }

    public void ResetTimer() =>
        _timeoutTimer.Change(TimeSpan.FromHours(1), TimeSpan.FromHours(1));

    private void TimeoutHandler(object data) =>
        TimerCallBackAction?.Invoke(this);
}
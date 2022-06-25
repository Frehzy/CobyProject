using ApiHostData.Domain.Models;

namespace ApiHostData.Cache.Entities
{
    public class SessionAction
    {
        private readonly Timer _timeoutTimer;

        public event Action<SessionAction> TimerCallBackAction;

        public Guid SessionId { get; private set; } = Guid.NewGuid();

        public OrderModel Order { get; private set; }

        public int Version { get; private set; } = 1;

        public SessionAction(OrderModel order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            _timeoutTimer = new Timer(TimeoutHandler, this, TimeSpan.FromHours(1), TimeSpan.FromHours(1));
        }

        public void UpdateOrder(OrderModel orderModel)
        {
            Order = orderModel;
            Order.Version = Order.Version + 1;
            Version++;
        }

        public void ResetTimer() =>
            _timeoutTimer.Change(TimeSpan.FromHours(1), TimeSpan.FromHours(1));

        public void Reset(OrderModel orderModel)
        {
            Order = orderModel;
            Version = 1;
        }

        private void TimeoutHandler(object data) =>
            TimerCallBackAction?.Invoke(this);
    }
}

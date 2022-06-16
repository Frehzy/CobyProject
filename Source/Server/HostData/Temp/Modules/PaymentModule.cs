namespace HostData.Modules;

/*public class PaymentModule : BaseModule
{
    private readonly IConfigSettings _configCache;
    private readonly PaymentController _paymentController;

    public PaymentModule(IOrderCache orderCache, IBaseCache<IWaiter> waiterCache, IBaseCache<IPaymentType> paymentTypeCache, IConfigSettings configCache) : base()
    {
        _configCache = configCache;
        _paymentController = new(orderCache, waiterCache, paymentTypeCache);

        Post("/{orderId}/payment/add/{credentialsId}/{paymentTypeId}/{sum}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var paymentTypeId = parameters.paymentTypeId;
            var sum = parameters.sum;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _paymentController.AddPayment(orderId, credentialsId, paymentTypeId, sum, obj));
        });

        Post("/{orderId}/payment/remove/{credentialsId}/{paymentId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var paymentId = parameters.paymentId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _paymentController.RemovePayment(orderId, credentialsId, paymentId, obj));
        });

        Get("/paymentTypes", parameters =>
        {
            return Execute(Context, () => _paymentController.GetPaymentTypes());
        });
    }
}*/
namespace HostData.Modules;

/*public class OrderModule : BaseModule
{
    private readonly IConfigSettings _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, IBaseCache<ITable> tableCache, IBaseCache<IWaiter> waiterCache, IConfigSettings configCache) : base()
    {
        _configCache = configCache;
        _orderController = new(orderCache, tableCache, waiterCache);

        Get("/orders/{orderId}", parameters =>
        {
            var orderId = parameters.orderId;
            return Execute<OrderDto>(Context, () => _orderController.GetOrderById(orderId));
        });

        Get("/allOrders", parameters =>
        {
            return Execute(Context, () => _orderController.GetOrders());
        });

        Get("/openOrders", parameters =>
        {
            return Execute(Context, () => _orderController.GetOpenOrders());
        });

        Get("/credentials/{waiterPassword}", parameters =>
        {
            var waiterPassword = parameters.waiterPassword;
            return Execute<CredentialsDto>(Context, () => _orderController.CreateCredentials(waiterPassword));
        });

        Get("/order/create/{credentialsId}/{waiterId}/{tablesId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            IEnumerable<dynamic> tablesId = parameters.tablesId.Split('/');
            return Execute<OrderDto>(Context, () => _orderController.CreateOrder(credentialsId, waiterId, tablesId));
        });

        Post("/order/remove/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<OrderDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.RemoveOrderById(credentialsId, obj.Id));
        });

        Post("/order/submitChanges/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.SubmitChanges(credentialsId, obj));
        });

        Post("/order/closeOrder/{credentialsId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<OrderDto>(Context, () => _orderController.CloseOrder(credentialsId, obj));
        });
    }
}*/
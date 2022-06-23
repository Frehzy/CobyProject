#nullable disable

using Api.Notification;
using Api.Operations.Contracts;
using Api.Operations.Implementation;
using Api.Services.Contrancts;
using Api.Services.Implementation;
using Shared.Configuration;
using Shared.Data;

namespace Api.Operations;

public sealed class ModuleOperation
{
    private static ModuleOperation _instance;

    private readonly IConfigSettings _configSettings;

    private readonly CredentialsOperation _credentialsOperation;
    private readonly DiscountTypeOperation _discountTypeOperation;
    private readonly OrderOperation _orderOperation;
    private readonly PaymentTypeOperation _paymentTypeOperation;
    private readonly ProductItemOperation _productItemOperation;
    private readonly TableOperation _tableOperation;
    private readonly WaiterOperation _waiterOperation;

    private readonly NotificationService _notificationService;

    private readonly OrderService _orderService;

    public IConfigSettings ConfigSettings => _configSettings;

    public ICredentialsOperation CredentialsOperation => _credentialsOperation;

    public IDiscountTypeOperation DiscountTypeOperation => _discountTypeOperation;

    public IOrderOperation OrderOperation => _orderOperation;

    public IPaymentTypeOperation PaymentTypeOperation => _paymentTypeOperation;

    public IProductItemOperation ProductItemOperation => _productItemOperation;

    public ITableOperation TableOperation => _tableOperation;

    public IWaiterOperation WaiterOperation => _waiterOperation;

    public INotificationService NotificationService => _notificationService;

    public static ModuleOperation GetInstance() =>
        _instance ??= new ModuleOperation();

    public ISessionOperation SessionOperation(ISession session) =>
        new SessionOperation(session, _orderService);

    private ModuleOperation()
    {
        var service = Configure.ConfigureServices();

        _configSettings = ConfigBuilder.Create();

        _credentialsOperation = (CredentialsOperation)service.GetService(typeof(ICredentialsOperation));
        _discountTypeOperation = (DiscountTypeOperation)service.GetService(typeof(IDiscountTypeOperation));
        _orderOperation = (OrderOperation)service.GetService(typeof(IOrderOperation));
        _paymentTypeOperation = (PaymentTypeOperation)service.GetService(typeof(IPaymentTypeOperation));
        _productItemOperation = (ProductItemOperation)service.GetService(typeof(IProductItemOperation));
        _tableOperation = (TableOperation)service.GetService(typeof(ITableOperation));
        _waiterOperation = (WaiterOperation)service.GetService(typeof(IWaiterOperation));

        _notificationService = (NotificationService)service.GetService(typeof(INotificationService));

        _orderService = (OrderService)service.GetService(typeof(IOrderService));

        ConnectToSignalR();
    }

    private async Task ConnectToSignalR()
    {
        if (_orderService.IsConnected is false)
            await _orderService.Connect();
    }

    ~ModuleOperation()
    {
        if (_orderService.IsConnected is true)
            _orderService.Disconnect();
        GC.Collect();
    }
}
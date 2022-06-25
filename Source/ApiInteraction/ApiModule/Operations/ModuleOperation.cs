#nullable disable

using ApiModule.Api;
using ApiModule.Attributes;
using ApiModule.Http;
using ApiModule.Operations.Contracts;
using ApiModule.Operations.Implementation;
using ApiModule.Services.Contrancts;
using ApiModule.Services.Implementation;
using Shared.Data;
using Shared.Exceptions;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace ApiModule.Operations;

public sealed class ModuleOperation
{
    private static ModuleOperation _instance;

    private readonly CredentialsOperation _credentialsOperation;
    private readonly DiscountTypeOperation _discountTypeOperation;
    private readonly OrderOperation _orderOperation;
    private readonly PaymentTypeOperation _paymentTypeOperation;
    private readonly ProductItemOperation _productItemOperation;
    private readonly TableOperation _tableOperation;
    private readonly WaiterOperation _waiterOperation;

    private readonly NotificationService _notificationService;

    private readonly OrderService _orderService;
    private readonly WaiterService _waiterService;

    public ICredentialsOperation CredentialsOperation => _credentialsOperation;

    public IDiscountTypeOperation DiscountTypeOperation => _discountTypeOperation;

    public IOrderOperation OrderOperation => _orderOperation;

    public IPaymentTypeOperation PaymentTypeOperation => _paymentTypeOperation;

    public IProductItemOperation ProductItemOperation => _productItemOperation;

    public ITableOperation TableOperation => _tableOperation;

    public IWaiterOperation WaiterOperation => _waiterOperation;

    public INotificationService NotificationService => _notificationService;

    [Obsolete($"Use {nameof(GetInstanceAsync)}")]
    public static ModuleOperation GetInstance()
    {
        try
        {
            _instance ??= new ModuleOperation();
            Task.Run(async () => await _instance.ConnectToSignalR()).GetAwaiter().GetResult();
            return _instance;
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
    }

    public static async Task<ModuleOperation> GetInstanceAsync()
    {
        _instance ??= new ModuleOperation();
        await _instance.ConnectToSignalR();
        return _instance;
    }

    public ISessionOperation SessionOperation(ISession session) =>
        new SessionOperation(session, _orderService);

    private ModuleOperation()
    {
        var licenceModuleId = CheckLicence();

        var service = BuildServiceProvider.ConfigureServices(licenceModuleId);

        _credentialsOperation = (CredentialsOperation)service.GetService(typeof(ICredentialsOperation));
        _discountTypeOperation = (DiscountTypeOperation)service.GetService(typeof(IDiscountTypeOperation));
        _orderOperation = (OrderOperation)service.GetService(typeof(IOrderOperation));
        _paymentTypeOperation = (PaymentTypeOperation)service.GetService(typeof(IPaymentTypeOperation));
        _productItemOperation = (ProductItemOperation)service.GetService(typeof(IProductItemOperation));
        _tableOperation = (TableOperation)service.GetService(typeof(ITableOperation));
        _waiterOperation = (WaiterOperation)service.GetService(typeof(IWaiterOperation));

        _notificationService = (NotificationService)service.GetService(typeof(INotificationService));

        _orderService = (OrderService)service.GetService(typeof(IOrderService));
        _waiterService = (WaiterService)service.GetService(typeof(IWaiterService));
    }

    private async Task ConnectToSignalR()
    {
        if (_orderService is null || _waiterService is null)
            throw new ArgumentNullException();

        if (_orderService.IsConnected is false)
            await _orderService.Connect();
        if (_waiterService.IsConnected is false)
            await _waiterService.Connect();
    }

    private int CheckLicence()
    {
        var licence = GetModuleLicence();
        HttpRequest.Request<List<LicenceDto>>($"moduleLicence/check/{ConfigSettings.CreateInstance().OrganizationId}/{licence.ModuleLicenceId}");
        return licence.ModuleLicenceId;

        LicenceModuleAttribute GetModuleLicence()
        {
            var licenceModuleAttributes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IIntegrationModule).IsAssignableFrom(x))
                .SelectMany(x =>
                {
                    var attributes = x.GetCustomAttributes(typeof(LicenceModuleAttribute), true);
                    return attributes.Cast<LicenceModuleAttribute>();
                });

            if (licenceModuleAttributes.Distinct().Count() != 1)
                throw new InvalidLicenceModuleException($"You must have only one [{nameof(LicenceModuleAttribute.ModuleLicenceId)}] " +
                                                        $"and inherit from the [{nameof(IIntegrationModule)}] interface");

            return licenceModuleAttributes.First();
        }
    }

    ~ModuleOperation()
    {
        if (_orderService?.IsConnected is true)
            _orderService.Disconnect().ConfigureAwait(false);
        if (_waiterService?.IsConnected is true)
            _waiterService.Disconnect().ConfigureAwait(false);
        GC.Collect();
    }
}
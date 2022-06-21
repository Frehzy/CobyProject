#nullable disable

using Api.Operations.Contracts;
using Api.Operations.Implementation;
using Shared.Configuration;
using Shared.Data;

namespace Api.Operations;

public sealed class ModuleOperation
{
    private static NetOperation _netOperation;
    private static IConfigSettings _configSettings;
    private static CredentialsOperation _credentialsOperation;
    private static DiscountTypeOperation _discountTypeOperation;
    private static OrderOperation _orderOperation;
    private static PaymentTypeOperation _paymentTypeOperation;
    private static ProductItemOperation _productItemOperation;
    private static TableOperation _tableOperation;
    private static WaiterOperation _waiterOperation;

    public static NetOperation NetOperation => _netOperation ??= new NetOperation();

    public static IConfigSettings ConfigSettings => _configSettings ??= ConfigBuilder.Create();

    public static ICredentialsOperation CredentialsOperation => _credentialsOperation ??= new CredentialsOperation();

    public static IDiscountTypeOperation DiscountTypeOperation => _discountTypeOperation ??= new DiscountTypeOperation();

    public static IOrderOperation OrderOperation => _orderOperation ??= new OrderOperation();

    public static IPaymentTypeOperation PaymentTypeOperation => _paymentTypeOperation ??= new PaymentTypeOperation();

    public static IProductItemOperation ProductItemOperation => _productItemOperation ??= new ProductItemOperation();

    public static ITableOperation TableOperation => _tableOperation ??= new TableOperation();

    public static IWaiterOperation WaiterOperation => _waiterOperation ??= new WaiterOperation();

    public static ISessionOperation SessionOperation(ISession session) =>
        new SessionOperation(session);
}
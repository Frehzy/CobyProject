using Api.Operations.GuestOper;
using Api.Operations.OrderOper;
using Api.Operations.ProductOper;
using Shared.Configuration;
using Shared.Data;

namespace Api.Operations;

public sealed class ModuleOperation
{
    private static NetOperation _netOperation;
    private static IConfigSettings _configSettings;
    private static OrderOperation _orderOperation;
    private static GuestOperation _guestOperation;
    private static ProductOperation _productOperation;

    public static NetOperation NetOperation => _netOperation ??= new NetOperation();

    public static IConfigSettings ConfigSettings => _configSettings ??= ConfigBuilder.Create();

    public static IOrderOperation OrderOperation => _orderOperation ??= new OrderOperation();

    public static IGuestOperation GuestOperation => _guestOperation ??= new GuestOperation();

    public static IProductOperation ProductOperation => _productOperation ??= new ProductOperation();
}
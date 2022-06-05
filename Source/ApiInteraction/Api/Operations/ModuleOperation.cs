using Api.Configuration;
using Api.Operations.GuestOper;
using Api.Operations.NetOper;
using Api.Operations.OrderOper;

namespace Api.Operations;

public sealed class ModuleOperation
{
    private static NetOperation _netOperation;
    private static IConfigSettings _configSettings;
    private static OrderOperation _orderOperation;
    private static GuestOperation _guestOperation;

    public static INetOperation NetOperation => _netOperation ??= new NetOperation();

    public static IConfigSettings ConfigSettings => _configSettings ??= ConfigBuilder.Create();

    public static IOrderOperation OrderOperation => _orderOperation ??= new OrderOperation();

    public static IGuestOperation GuestOperation => _guestOperation ??= new GuestOperation();
}
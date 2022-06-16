namespace HostData.Controllers;

/*internal class TableController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<ITable> _tableCache;

    public TableController(IOrderCache orderCache, IBaseCache<ITable> tableCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _tableCache = tableCache;
    }

    public async Task<List<TableDto>> GetTables()
    {
        return _tableCache.Values.Select(x => TableFactory.CreateDto(x)).ToList();
    }

    public async Task<SessionDto> ChangeTable(dynamic orderId, dynamic credentialsId, IEnumerable<dynamic> tablesId, SessionDto session)
    {
        var oId = (Guid)CheckDynamicGuid(orderId);
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var tsId = tablesId.Select(x => (Guid)CheckDynamicGuid(x));

        CheckCredentials(cId, EmployeePermission.CanChangeTableOnOrder).ConfigureAwait(false);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        var tables = tsId.Select(x => TableFactory.CreateDto(_tableCache.GetById(x)));

        var newOrder = order with { Tables = tables.ToList(), Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return session with { Version = session.Version + 1 };
    }
}*/
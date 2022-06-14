using HostData.Cache;
using HostData.Cache.Order;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class TableController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<ITable> _tableCache;

    public TableController(IOrderCache orderCache, IBaseCache<ITable> tableCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _tableCache = tableCache;
    }

    public Task<List<TableDto>> GetTables()
    {
        return Task.FromResult(_tableCache.Values.Select(x => TableFactory.CreateDto(x)).ToList());
    }

    public Task<SessionDto> ChangeTable(dynamic orderId, dynamic credentialsId, IEnumerable<dynamic> tablesId, SessionDto session)
    {
        var oId = CheckDynamicGuid(orderId);
        var cId = CheckDynamicGuid(credentialsId);
        var tsId = tablesId.Select(x => (Guid)CheckDynamicGuid(x)).ToList();

        CheckCredentials(cId, EmployeePermission.CanChangeTableOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        var tables = tsId.Select(x => TableFactory.CreateDto(_tableCache.GetById(x)));

        var newOrder = order with { Tables = tables.ToList(), Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return Task.FromResult(session with { Version = session.Version + 1 });
    }
}
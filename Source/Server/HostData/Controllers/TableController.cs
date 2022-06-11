using HostData.Cache.Orders;
using HostData.Cache.Tables;
using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class TableController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly ITableCache _tableCache;

    public TableController(IOrderCache orderCache, ITableCache tableCache, IWaiterCache waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _tableCache = tableCache;
    }

    public async Task<List<TableDto>> GetTables()
    {
        return await Task.Run(() =>
        {
            return _tableCache.Tables.Select(x => TableFactory.CreateDto(x)).ToList();
        });
    }

    internal async Task<SessionDto> ChangeTable(dynamic orderId, dynamic credentialsId, IEnumerable<dynamic> tablesId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var tsId = tablesId.Select(x => (Guid)CheckDynamicGuid(x)).ToList();

            CheckCredentials(cId, EmployeePermission.CanChangeTableOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));

            var tables = tsId.Select(x => TableFactory.CreateDto(_tableCache.GetTableById(x)));

            var newOrder = order with { Tables = tables.ToList(), Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }
}
using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Cache.Tables;
using HostData.Cache.Waiters;
using HostData.Controllers;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class TableModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly TableController _tableController;

    public TableModule(IOrderCache orderCache, ITableCache tableCache, IWaiterCache waiterCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _tableController = new(orderCache, tableCache, waiterCache);

        Post("/{orderId}/table/changeTable/{credentialsId}/{tablesId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            IEnumerable<dynamic> tablesId = parameters.tablesId.Split('/');
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _tableController.ChangeTable(orderId, credentialsId, tablesId, obj));
        });

        Get("/tables", parameters =>
        {
            return Execute(Context, () => _tableController.GetTables());
        });
    }
}
using HostData.Cache;
using HostData.Cache.Order;
using HostData.Controllers;
using Nancy.Extensions;
using Shared.Data;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class TableModule : BaseModule
{
    private readonly IConfigSettings _configCache;
    private readonly TableController _tableController;

    public TableModule(IOrderCache orderCache, IBaseCache<ITable> tableCache, IBaseCache<IWaiter> waiterCache, IConfigSettings configCache) : base()
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
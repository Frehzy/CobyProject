using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.TableOper;

internal class TableOperation : ITableOperation
{
    public IReadOnlyList<ITable> GetTables()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "tables");
        var result = HttpRequest.Get<List<TableDto>>(uri);
        return result.Content.Select(x => TableFactory.Create(x)).ToList();
    }
}
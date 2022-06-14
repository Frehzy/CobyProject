using Api.Http;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.TableOper;

internal class TableOperation : ITableOperation
{
    public IReadOnlyList<ITable> ChangeTable(ICredentials credentials, IReadOnlyList<ITable> tables, ref ISession session)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{session.OrderId}/table/changeTable/{credentials.Id}/{string.Join("/", tables.Select(x => x.Id))}");
        var sessionDto = SessionFactory.CreateDto(session);
        var result = Task.Run(async () => await HttpRequest.Post(uri, sessionDto)).Result;
        session = SessionFactory.Create(result.Content);
        return session.Orders.OrderByDescending(x => x.Version).SelectMany(x => x.GetTables()).ToList();
    }

    public IReadOnlyList<ITable> GetTables()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "tables");
        var result = Task.Run(async () => await HttpRequest.Get<List<TableDto>>(uri)).Result;
        return result.Content.Select(x => TableFactory.Create(x)).ToList();
    }
}
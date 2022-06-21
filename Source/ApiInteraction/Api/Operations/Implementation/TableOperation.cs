using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class TableOperation : ITableOperation
{
    public ITable CreateTable(ICredentials credentials, int tableNumber, string tableName)
    {
        var result = Request<TableDto>($"{credentials.Id}/table/create/{tableNumber}/{tableName}");
        return TableFactory.Create(result);
    }

    public ITable GetTableById(Guid tableId)
    {
        var result = Request<TableDto>($"table/{tableId}");
        return TableFactory.Create(result);
    }

    public IReadOnlyList<ITable> GetTables()
    {
        var result = Request<List<TableDto>>($"tables");
        return result.Select(x => TableFactory.Create(x)).ToList();
    }

    public bool RemoveTable(ICredentials credentials, ITable table)
    {
        return Request<TableDto>($"{credentials.Id}/table/remove/{table.Id}") is not null;
    }

    private T Request<T>(string path)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, path);
        var result = Task.Run(async () => await HttpRequest.Get<T>(uri)).Result;
        return result.Content;
    }
}
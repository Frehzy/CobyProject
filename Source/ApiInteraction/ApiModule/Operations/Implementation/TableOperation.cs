using ApiModule.Http;
using ApiModule.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace ApiModule.Operations.Implementation;

internal class TableOperation : ITableOperation
{
    public ITable CreateTable(ICredentials credentials, int tableNumber, string tableName)
    {
        var result = HttpRequest.Request<TableDto>($"{credentials.Id}/table/create/{tableNumber}/{tableName}");
        return TableFactory.Create(result);
    }

    public ITable GetTableById(Guid tableId)
    {
        var result = HttpRequest.Request<TableDto>($"table/{tableId}");
        return TableFactory.Create(result);
    }

    public IReadOnlyList<ITable> GetTables()
    {
        var result = HttpRequest.Request<List<TableDto>>($"tables");
        return result.Select(x => TableFactory.Create(x)).ToList();
    }

    public bool RemoveTable(ICredentials credentials, ITable table)
    {
        return HttpRequest.Request<TableDto>($"{credentials.Id}/table/remove/{table.Id}") is not null;
    }
}
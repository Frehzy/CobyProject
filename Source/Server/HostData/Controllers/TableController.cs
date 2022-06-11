using HostData.Cache.Tables;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class TableController
{
    private readonly ITableCache _tableCache;

    public TableController(ITableCache tableCache)
    {
        _tableCache = tableCache;
    }

    public async Task<List<TableDto>> GetTables()
    {
        return await Task.Run(() =>
        {
            return _tableCache.Tables.Select(x => TableFactory.CreateDto(x)).ToList();
        });
    }
}
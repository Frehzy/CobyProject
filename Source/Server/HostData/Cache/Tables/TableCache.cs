using Shared.Data;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache.Tables;

internal class TableCache : ITableCache
{
    private readonly ConcurrentDictionary<Guid, ITable> _tablesCache = new();

    public IReadOnlyCollection<ITable> Tables => _tablesCache.Values.ToList();

    public ITable GetTableById(Guid tableId)
    {
        if (_tablesCache.TryGetValue(tableId, out var tableOnCache) is false)
            throw new EntityNotFoundException(tableId, nameof(ITable));
        return tableOnCache;
    }

    public void AddOrUpdate(ITable table)
    {
        if (_tablesCache.TryGetValue(table.Id, out var tableOnCache) is false)
            _tablesCache.TryAdd(table.Id, table);
        else
            _tablesCache.TryUpdate(table.Id, table, tableOnCache);
    }

    public ITable RemoveTable(Guid tableId)
    {
        var table = GetTableById(tableId);

        if (table.IsDeleted is true)
            throw new CantRemoveDeletedItemException(table.Id);

        var tableDto = TableFactory.CreateDto(table);
        tableDto = tableDto with { IsDeleted = true };

        AddOrUpdate(TableFactory.Create(tableDto));
        return GetTableById(tableId);
    }
}
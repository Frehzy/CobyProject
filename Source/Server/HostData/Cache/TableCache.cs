using Shared.Data;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache;

internal class TableCache : IBaseCache<ITable>
{
    private readonly ConcurrentDictionary<Guid, ITable> _tablesCache = new();

    public IReadOnlyCollection<ITable> Values => _tablesCache.Values.ToList();

    public ITable GetById(Guid tableId)
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

    public ITable RemoveById(Guid tableId)
    {
        var table = GetById(tableId);

        if (table.IsDeleted is true)
            throw new CantRemoveDeletedItemException(table.Id);

        var tableDto = TableFactory.CreateDto(table);
        tableDto = tableDto with { IsDeleted = true };

        AddOrUpdate(TableFactory.Create(tableDto));
        return GetById(tableId);
    }
}
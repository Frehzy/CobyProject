using Shared.Data;

namespace HostData.Cache.Tables;

public interface ITableCache
{
    IReadOnlyCollection<ITable> Tables { get; }

    ITable GetTableById(Guid tableId);

    void AddOrUpdate(ITable table);

    ITable RemoveTable(Guid tableId);
}
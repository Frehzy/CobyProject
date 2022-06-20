using Shared.Data;

namespace Api.Operations.Contracts;

public interface ITableOperation
{
    public IReadOnlyList<ITable> GetTables();

    public ITable GetTableById(Guid tableId);

    public ITable CreateTable(ICredentials credentials, int tableNumber, string tableName);

    public bool RemoveTable(ICredentials credentials, ITable table);
}
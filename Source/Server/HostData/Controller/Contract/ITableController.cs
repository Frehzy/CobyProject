using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface ITableController
{
    public Task<TableDto> CreateTable(dynamic credentials, dynamic tableNumber, dynamic tableName);

    public Task<TableDto> RemoveTableById(dynamic credentials, dynamic tableId);

    public Task<TableDto> GetTableById(dynamic tableId);

    public Task<List<TableDto>> GetTables();
}
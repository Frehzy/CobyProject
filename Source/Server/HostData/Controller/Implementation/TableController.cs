using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class TableController : BaseController, ITableController
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService, IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache) 
        : base(waiterService, mapper, credentialsCache)
    {
        _tableService = tableService;
    }

    public async Task<TableDto> CreateTable(dynamic credentials, dynamic tableNumber, dynamic tableName)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var tNumber = int.Parse(tableNumber);
        var tName = (string)Convert.ToString(tableName);
        var entityThatChanges = await CheckCredentials(cId);

        var tableModel = new TableModel()
        {
            Number = tNumber,
            Name = tName
        };
        await _tableService.Create(entityThatChanges.Id, tableModel);
        return Mapper.Map<TableModel, TableDto>(tableModel);
    }

    public async Task<TableDto> GetTableById(dynamic tableId)
    {
        var tId = (Guid)CheckDynamicGuid(tableId);
        var tableModel = await _tableService.GetById(tId);
        return Mapper.Map<TableModel, TableDto>(tableModel);
    }

    public async Task<List<TableDto>> GetTables()
    {
        var tableModels = await _tableService.GetAll();
        return tableModels.Select(x => Mapper.Map<TableModel, TableDto>(x)).ToList();
    }

    public async Task<TableDto> RemoveTableById(dynamic credentials, dynamic tableId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var tId = (Guid)CheckDynamicGuid(tableId);

        var entityThatChanges = await CheckCredentials(cId);

        var tableModel = await _tableService.GetById(tId);

        await _tableService.Remove(entityThatChanges.Id, tId);
        return Mapper.Map<TableModel, TableDto>(tableModel);
    }
}
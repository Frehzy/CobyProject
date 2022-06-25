using ApiHostData.Cache.Credentials;
using ApiHostData.Controller.Contract;
using ApiHostData.Domain.Models;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Shared.Factory.Dto;
using SharedData.Mapper;

namespace ApiHostData.Controller.Implementation;

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
        Guid cId = CheckDynamicGuid(credentials);
        int tNumber = int.Parse(tableNumber);
        string tName = Convert.ToString(tableName.ToString());
        var entityThatChanges = await CheckCredentials(cId);

        var tableModel = new TableModel()
        {
            Number = tNumber,
            Name = tName
        };
        await _tableService.Create(entityThatChanges.Id, tableModel);
        return TableFactory.CreateDto(tableModel);
    }

    public async Task<TableDto> GetTableById(dynamic tableId)
    {
        Guid tId = CheckDynamicGuid(tableId);
        var tableModel = await _tableService.GetById(tId);
        return TableFactory.CreateDto(tableModel);
    }

    public async Task<List<TableDto>> GetTables()
    {
        var tableModels = await _tableService.Get();
        return tableModels.Select(x => TableFactory.CreateDto(x)).ToList();
    }

    public async Task<TableDto> RemoveTableById(dynamic credentials, dynamic tableId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid tId = CheckDynamicGuid(tableId);

        var entityThatChanges = await CheckCredentials(cId);

        var tableModel = await _tableService.GetById(tId);

        await _tableService.Remove(entityThatChanges.Id, tId);
        return TableFactory.CreateDto(tableModel);
    }
}
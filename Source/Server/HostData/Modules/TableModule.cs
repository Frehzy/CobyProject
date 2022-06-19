using HostData.Controller.Contract;
using Shared.Factory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostData.Modules;

public class TableModule : BaseModule
{
    private readonly ITableController _tableController;

    public TableModule(ITableController tableController) : base()
    {
        _tableController = tableController;

        Get("/tables", async parameters =>
        {
            return await Execute(Context, () => _tableController.GetTables());
        });

        Get("/table/{tableId}", async parameters =>
        {
            var tableId = parameters.tableId;
            return await Execute<TableDto>(Context, () => _tableController.GetTableById(tableId));
        });

        Get("{credentialsId}/table/create/{tableNumber}/{tableName}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var tableNumber = parameters.tableNumber;
            var tableName = parameters.tableName;
            return await Execute<TableDto>(Context, () => _tableController.CreateTable(credentialsId, tableNumber, tableName));
        });

        Get("{credentialsId}/table/remove/{tableId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var tableId = parameters.tableId;
            return await Execute<TableDto>(Context, () => _tableController.RemoveTableById(credentialsId, tableId));
        });
    }
}
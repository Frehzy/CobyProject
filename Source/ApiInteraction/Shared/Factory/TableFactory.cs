using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class TableFactory
{
    public static Table Create(ITable table) =>
        new(table.Id, table.Name, table.Number);

    public static TableDto CreateDto(ITable table) =>
        new(table.Id, table.Name, table.Number);

    public static Table Create(TableDto table) =>
        new(table.Id, table.Name, table.Number);
}
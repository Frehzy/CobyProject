using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Factory;

public static class TableFactory
{
    public static TableDto CreateDto(TableModel tableModel) =>
        new(tableModel.Id,
            tableModel.Name,
            tableModel.Number);
}
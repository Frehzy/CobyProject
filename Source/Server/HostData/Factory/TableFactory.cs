using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Factory;

public static class TableFactory
{
    public static TableDto CreateDto(TableModel tableModel) =>
        new(tableModel.Id,
            tableModel.Name,
            tableModel.Number);
}
using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Factory;

public static class WaiterFactory
{
    public static WaiterDto CreateDto(WaiterModel waiterModel) =>
        new(waiterModel.Id,
            waiterModel.Name,
            waiterModel.IsSessionOpen,
            waiterModel.Permissions,
            waiterModel.IsDeleted);
}
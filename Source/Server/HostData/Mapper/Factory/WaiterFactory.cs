using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Mapper.Factory;

internal static class WaiterFactory
{
    internal static WaiterDto CreateDto(WaiterPermissionModel model) =>
        new(model.Id,
            model.Waiter.Name,
            model.Waiter.IsSessionOpen,
            model.Permissions.Select(x => x.EmployeePermission).ToList(),
            model.Waiter.IsDeleted);

    internal static WaiterDto CreateDto(WaiterModel model) =>
        new(model.Id,
            model.Name,
            model.IsSessionOpen,
            new(),
            model.IsDeleted);
}
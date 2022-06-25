using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Factory;

public static class WaiterFactory
{
    public static WaiterDto CreateDto(WaiterModel waiterModel) =>
        new(waiterModel.Id,
            waiterModel.Name,
            waiterModel.IsSessionOpen,
            waiterModel.Permissions);
}
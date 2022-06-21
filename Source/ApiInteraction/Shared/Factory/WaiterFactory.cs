using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class WaiterFactory
{
    public static Waiter Create(IWaiter waiter) =>
        new(waiter.Id, waiter.Name, waiter.IsSessionOpen, waiter.GetPermissions().ToList());

    public static WaiterDto CreateDto(IWaiter waiter) =>
        new(waiter.Id, waiter.Name, waiter.IsSessionOpen, waiter.GetPermissions().ToList());

    public static Waiter Create(WaiterDto waiter) =>
        new(waiter.Id, waiter.Name, waiter.IsSessionOpen, waiter.GetPermissions());
}
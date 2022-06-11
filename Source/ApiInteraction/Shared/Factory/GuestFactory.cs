using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal static class GuestFactory
{
    public static Guest Create(IGuest guest) =>
        new(guest.Id, guest.Name, guest.Rank, guest.IsDeleted);

    public static GuestDto CreateDto(IGuest guest) =>
        new(guest.Id, guest.Name, guest.Rank, guest.IsDeleted);

    public static Guest Create(GuestDto guest) =>
        new(guest.Id, guest.Name, guest.Rank, guest.IsDeleted);
}
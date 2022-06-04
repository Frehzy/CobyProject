using Api.Data.Guest;
using Api.Factory.InternalModel;

namespace Api.Factory;

internal static class GuestFactory
{
    public static Guest Create(IGuest guest) =>
        new(guest.Id, guest.Name, guest.Rank);
}
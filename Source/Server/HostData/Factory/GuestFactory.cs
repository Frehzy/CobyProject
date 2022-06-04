using Api.Data.Guest;
using HostData.Model;

namespace HostData.Factory;

internal static class GuestFactory
{
    public static Guest Create(IGuest guest) =>
        new(guest.Id, guest.Name, guest.Rank);
}
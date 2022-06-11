using Shared.Data;

namespace Api.Operations.GuestOper;

public interface IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(IOrder order, ref ISession session);

    public IReadOnlyList<IGuest> RemoveGuest(IOrder order, IGuest guest, ref ISession session);
}
using Shared.Data;

namespace Api.Operations.GuestOper;

public interface IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(IOrder order, ref ISession session);

    public bool RemoveGuest(IOrder order, Guid guestId, ref ISession session);
}
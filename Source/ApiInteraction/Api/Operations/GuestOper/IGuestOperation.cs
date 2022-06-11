using Shared.Data;

namespace Api.Operations.GuestOper;

public interface IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(IOrder order, ICredentials credentials, ref ISession session);

    public IReadOnlyList<IGuest> RemoveGuest(IOrder order, ICredentials credentials, IGuest guest, ref ISession session);
}
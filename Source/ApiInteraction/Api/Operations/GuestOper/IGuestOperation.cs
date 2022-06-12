using Shared.Data;

namespace Api.Operations.GuestOper;

public interface IGuestOperation
{
    public IReadOnlyList<IGuest> CreateGuest(ICredentials credentials, ref ISession session);

    public IReadOnlyList<IGuest> RemoveGuest(ICredentials credentials, IGuest guest, ref ISession session);
}
using Api.Data;
using Api.Data.Order;

namespace Api.Operations.GuestOper;

public interface IGuestOperation
{
    public ISession CreateGuest(IOrder order, ISession session);

    public bool RemoveGuest(IOrder order, Guid guestId, ISession session);
}
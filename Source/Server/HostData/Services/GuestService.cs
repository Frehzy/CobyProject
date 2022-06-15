using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class GuestService : BaseService, IGuestService
{
    public GuestService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(GuestModel guest) =>
        await base.Create<GuestModel, GuestEntity>(guest);

    public async Task Delete(Guid id) =>
        await base.Delete<GuestEntity>(id);

    public async Task Update(GuestModel guest) =>
        await base.Update<GuestModel, GuestEntity>(guest);

    public async Task<GuestModel> Get(Guid id) =>
        await base.GetById<GuestModel, GuestEntity>(id);

    public async Task<List<GuestModel>> GetAll() =>
        await base.GetAll<GuestModel, GuestEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<GuestEntity>(id);
}
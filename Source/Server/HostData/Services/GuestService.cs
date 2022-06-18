using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class GuestService : BaseService, IGuestService
{
    public GuestService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, GuestModel guest) =>
        await base.Create<GuestModel, GuestEntity>(entityThatChangesId, guest);

    public async Task Delete(Guid id) =>
        await base.Delete<GuestEntity>(id);

    public async Task Update(Guid entityThatChangesId, GuestModel guest) =>
        await base.Update<GuestModel, GuestEntity>(entityThatChangesId, guest);

    public async Task<GuestModel> GetById(Guid id) =>
        await base.GetById<GuestModel, GuestEntity>(id);

    public async Task<List<GuestModel>> GetAll() =>
        await base.GetAll<GuestModel, GuestEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<GuestEntity>(entityThatChangesId, id);
}
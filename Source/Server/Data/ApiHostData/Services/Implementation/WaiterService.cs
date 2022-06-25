using ApiHostData.Domain.Entities;
using ApiHostData.Domain.Models;
using ApiHostData.Repository;
using ApiHostData.Services.Contract;
using Shared.Data;
using Shared.Exceptions;
using SharedData.Mapper;

namespace ApiHostData.Services.Implementation;

public class WaiterService : BaseService, IWaiterService
{
    public WaiterService(IApiHostRepository dbRepository, IMapper mapper) : base(dbRepository, mapper) { }

    public async Task<Guid> Create(Guid entityThatChangesId, WaiterModel waiter)
    {
        await CheckIfExists(waiter);
        return await base.Create<WaiterModel, WaiterEntity>(entityThatChangesId, waiter);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<WaiterEntity>(id);

    public async Task Update(Guid entityThatChangesId, WaiterModel waiter)
    {
        await CheckIfExists(waiter);
        await base.Update<WaiterModel, WaiterEntity>(entityThatChangesId, waiter);
    }

    public async Task<WaiterModel> GetById(Guid id) =>
        await base.GetById<WaiterModel, WaiterEntity>(id);

    public async Task<List<WaiterModel>> Get() =>
        await base.Get<WaiterModel, WaiterEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<WaiterEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(WaiterModel waiter)
    {
        var entity = Mapper.Map<WaiterModel, WaiterEntity>(waiter);
        if (await base.CheckIfExists(entity, x => x.Name.Equals(waiter.Name) || x.Password.Equals(waiter.Password)) is true)
            throw new EntityAlreadyExistsException(waiter.Id, typeof(IWaiter).ToString());
    }
}
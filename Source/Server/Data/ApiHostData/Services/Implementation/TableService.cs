using ApiHostData.Domain.Entities;
using ApiHostData.Domain.Models;
using ApiHostData.Repository;
using ApiHostData.Services.Contract;
using Shared.Data;
using Shared.Exceptions;
using SharedData.Mapper;

namespace ApiHostData.Services.Implementation;

public class TableService : BaseService, ITableService
{
    public TableService(IApiHostRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, TableModel table)
    {
        await CheckIfExists(table);
        return await base.Create<TableModel, TableEntity>(entityThatChangesId, table);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<TableEntity>(id);

    public async Task Update(Guid entityThatChangesId, TableModel table)
    {
        await CheckIfExists(table);
        await base.Update<TableModel, TableEntity>(entityThatChangesId, table);
    }

    public async Task<TableModel> GetById(Guid id) =>
        await base.GetById<TableModel, TableEntity>(id);

    public async Task<List<TableModel>> Get() =>
        await base.Get<TableModel, TableEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<TableEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(TableModel table)
    {
        var entity = Mapper.Map<TableModel, TableEntity>(table);
        if (await base.CheckIfExists(entity, x => x.Number.Equals(table.Number) || x.Name.Equals(table.Name)) is true)
            throw new EntityAlreadyExistsException(table.Id, typeof(ITable).ToString());
    }
}
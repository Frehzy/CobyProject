using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using Shared.Data;
using Shared.Exceptions;

namespace HostData.Services;

public class TableService : BaseService, ITableService
{
    public TableService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
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

    public async Task<List<TableModel>> GetAll() =>
        await base.GetAll<TableModel, TableEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<TableEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(TableModel table)
    {
        var entity = Mapper.Map<TableModel, TableEntity>(table);
        if (await base.CheckIfExists(entity, x => x.Number.Equals(table.Number) || x.Name.Equals(table.Name)) is true)
            throw new EntityAlreadyExistsException(table.Id, typeof(ITable).ToString());
    }
}
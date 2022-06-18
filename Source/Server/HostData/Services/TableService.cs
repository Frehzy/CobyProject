using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class TableService : BaseService, ITableService
{
    public TableService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, TableModel table) =>
        await base.Create<TableModel, TableEntity>(entityThatChangesId, table);

    public async Task Delete(Guid id) =>
        await base.Delete<TableEntity>(id);

    public async Task Update(Guid entityThatChangesId, TableModel table) =>
        await base.Update<TableModel, TableEntity>(entityThatChangesId, table);

    public async Task<TableModel> GetById(Guid id) =>
        await base.GetById<TableModel, TableEntity>(id);

    public async Task<List<TableModel>> GetAll() =>
        await base.GetAll<TableModel, TableEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<TableEntity>(entityThatChangesId, id);
}
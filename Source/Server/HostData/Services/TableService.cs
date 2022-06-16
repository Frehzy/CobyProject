using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class TableService : BaseService, ITableService
{
    public TableService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) 
        : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(TableModel table) =>
        await base.Create<TableModel, TableEntity>(table);

    public async Task Delete(Guid id) =>
        await base.Delete<TableEntity>(id);

    public async Task Update(TableModel table) =>
        await base.Update<TableModel, TableEntity>(table);

    public async Task<TableModel> GetById(Guid id) =>
        await base.GetById<TableModel, TableEntity>(id);

    public async Task<List<TableModel>> GetAll() =>
        await base.GetAll<TableModel, TableEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<TableEntity>(id);
}
using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class ProductService : BaseService, IProductService
{
    public ProductService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(ProductModel product) =>
        await base.Create<ProductModel, ProductEntity>(product);

    public async Task Delete(Guid id) =>
        await base.Delete<ProductEntity>(id);

    public async Task Update(ProductModel product) =>
        await base.Update<ProductModel, ProductEntity>(product);

    public async Task<ProductModel> GetById(Guid id) =>
        await base.GetById<ProductModel, ProductEntity>(id);

    public async Task<List<ProductModel>> GetAll() =>
        await base.GetAll<ProductModel, ProductEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<ProductEntity>(id);
}
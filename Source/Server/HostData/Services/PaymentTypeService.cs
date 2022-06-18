using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class PaymentTypeService : BaseService, IPaymentTypeService
{
    public PaymentTypeService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, PaymentTypeModel paymentType) =>
        await base.Create<PaymentTypeModel, PaymentTypeEntity>(entityThatChangesId, paymentType);

    public async Task Delete(Guid id) =>
        await base.Delete<PaymentTypeEntity>(id);

    public async Task Update(Guid entityThatChangesId, PaymentTypeModel paymentType) =>
        await base.Update<PaymentTypeModel, PaymentTypeEntity>(entityThatChangesId, paymentType);

    public async Task<PaymentTypeModel> GetById(Guid id) =>
        await base.GetById<PaymentTypeModel, PaymentTypeEntity>(id);

    public async Task<List<PaymentTypeModel>> GetAll() =>
        await base.GetAll<PaymentTypeModel, PaymentTypeEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<PaymentTypeEntity>(entityThatChangesId, id);
}
using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class PaymentTypeService : BaseService, IPaymentTypeService
{
    public PaymentTypeService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(PaymentTypeModel paymentType) =>
        await base.Create<PaymentTypeModel, PaymentTypeEntity>(paymentType);

    public async Task Delete(Guid id) =>
        await base.Delete<PaymentTypeEntity>(id);

    public async Task Update(PaymentTypeModel paymentType) =>
        await base.Update<PaymentTypeModel, PaymentTypeEntity>(paymentType);

    public async Task<PaymentTypeModel> GetById(Guid id) =>
        await base.GetById<PaymentTypeModel, PaymentTypeEntity>(id);

    public async Task<List<PaymentTypeModel>> GetAll() =>
        await base.GetAll<PaymentTypeModel, PaymentTypeEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<PaymentTypeEntity>(id);
}
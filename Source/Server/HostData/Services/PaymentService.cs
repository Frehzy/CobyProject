using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class PaymentService : BaseService, IPaymentService
{
    public PaymentService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(PaymentModel payment) =>
        await base.Create<PaymentModel, OrderPaymentEntity>(payment);

    public async Task Delete(Guid id) =>
        await base.Delete<OrderPaymentEntity>(id);

    public async Task Update(PaymentModel payment) =>
        await base.Update<PaymentModel, OrderPaymentEntity>(payment);

    public async Task<PaymentModel> GetById(Guid id) =>
        await base.GetById<PaymentModel, OrderPaymentEntity>(id);

    public async Task<List<PaymentModel>> GetAll() =>
        await base.GetAll<PaymentModel, OrderPaymentEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<OrderPaymentEntity>(id);
}
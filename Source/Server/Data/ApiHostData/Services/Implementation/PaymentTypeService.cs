using ApiHostData.Domain.Entities;
using ApiHostData.Domain.Models;
using ApiHostData.Repository;
using ApiHostData.Services.Contract;
using Shared.Data;
using Shared.Exceptions;
using SharedData.Mapper;

namespace ApiHostData.Services.Implementation;

public class PaymentTypeService : BaseService, IPaymentTypeService
{
    public PaymentTypeService(IApiHostRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, PaymentTypeModel paymentType)
    {
        await CheckIfExists(paymentType);
        return await base.Create<PaymentTypeModel, PaymentTypeEntity>(entityThatChangesId, paymentType);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<PaymentTypeEntity>(id);

    public async Task Update(Guid entityThatChangesId, PaymentTypeModel paymentType)
    {
        await CheckIfExists(paymentType);
        await base.Update<PaymentTypeModel, PaymentTypeEntity>(entityThatChangesId, paymentType);
    }

    public async Task<PaymentTypeModel> GetById(Guid id) =>
        await base.GetById<PaymentTypeModel, PaymentTypeEntity>(id);

    public async Task<List<PaymentTypeModel>> Get() =>
        await base.Get<PaymentTypeModel, PaymentTypeEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<PaymentTypeEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(PaymentTypeModel table)
    {
        var entity = Mapper.Map<PaymentTypeModel, PaymentTypeEntity>(table);
        if (await base.CheckIfExists(entity, x => x.Name.Equals(table.Name)) is true)
            throw new EntityAlreadyExistsException(table.Id, typeof(IPaymentType).ToString());
    }
}
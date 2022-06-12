using Shared.Data;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache.Payments;

internal class PaymentTypeCache : IPaymentTypeCache
{
    private readonly ConcurrentDictionary<Guid, IPaymentType> _paymentTypesCache = new();

    public IReadOnlyCollection<IPaymentType> PaymentTypes => _paymentTypesCache.Values.ToList();

    public void AddOrUpdate(IPaymentType paymentType)
    {
        if (_paymentTypesCache.TryGetValue(paymentType.Id, out var paymentOnCache) is false)
            _paymentTypesCache.TryAdd(paymentType.Id, paymentType);
        else
            _paymentTypesCache.TryUpdate(paymentType.Id, paymentType, paymentOnCache);
    }

    public IPaymentType GetPaymentTypeById(Guid paymentTypeId)
    {
        if (_paymentTypesCache.TryGetValue(paymentTypeId, out var returnPaymentType) is false)
            throw new EntityNotFoundException(paymentTypeId, nameof(IPaymentType));
        return returnPaymentType;
    }

    public IPaymentType RemovePaymentType(Guid paymentTypeId)
    {
        var paymentType = GetPaymentTypeById(paymentTypeId);

        if (paymentType.IsDeleted is true)
            throw new CantRemoveDeletedItemException(paymentType.Id);

        var paymentTypeDto = PaymentTypeFactory.CreateDto(paymentType);
        paymentTypeDto = paymentTypeDto with { IsDeleted = true };

        AddOrUpdate(PaymentTypeFactory.Create(paymentTypeDto));
        return GetPaymentTypeById(paymentTypeId);
    }
}
using HostData.Cache;
using HostData.Cache.Order;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class PaymentController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<IPaymentType> _paymentTypeCache;

    public PaymentController(IOrderCache orderCache, IBaseCache<IWaiter> waiterCache, IBaseCache<IPaymentType> paymentTypeCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _paymentTypeCache = paymentTypeCache;
    }

    public async Task<SessionDto> AddPayment(dynamic orderId, dynamic credentialsId, dynamic paymentTypeId, dynamic sum, SessionDto session)
    {
        var oId = (Guid)CheckDynamicGuid(orderId);
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var ptId = (Guid)CheckDynamicGuid(paymentTypeId);

        if (decimal.TryParse(sum.ToString(), out decimal returnSum) is false)
            throw new ArgumentException($"{nameof(sum)} must be type Decimal", nameof(sum));

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        var waiter = await CheckCredentials(cId, EmployeePermission.CanAddPaymentOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        PaymentTypeDto paymentType = PaymentTypeFactory.CreateDto(_paymentTypeCache.GetById(ptId));

        var paymentsList = session.Orders.Count <= 0
            ? order.GetPayments()
            : session.Orders.OrderByDescending(x => x.Version).First().GetPayments();

        var paymentDto = new PaymentDto(Guid.NewGuid(), returnSum, paymentType, PaymentStatus.New, false);

        paymentsList.Add(paymentDto);
        var newOrder = order with { Payments = paymentsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return session with { Version = session.Version + 1 };
    }

    public async Task<SessionDto> RemovePayment(dynamic orderId, dynamic credentialsId, dynamic paymentId, SessionDto session)
    {
        var oId = (Guid)CheckDynamicGuid(orderId);
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var pId = (Guid)CheckDynamicGuid(paymentId);

        if (session.OrderId.Equals(oId) is false)
            throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

        var waiter = await CheckCredentials(cId, EmployeePermission.CanRemovePaymentOnOrder);

        OrderDto order = OrderFactory.CreateDto(_orderCache.GetById(oId));

        var paymentsList = session.Orders.Count <= 0
            ? order.GetPayments()
            : session.Orders.OrderByDescending(x => x.Version).First().GetPayments();

        var payment = paymentsList.First(x => x.Id.Equals(pId));
        if (payment.IsDeleted is true)
            throw new CantRemoveDeletedItemException(payment.Id);
        payment = payment with { Status = PaymentStatus.Removed, IsDeleted = true };

        var newOrder = order with { Payments = paymentsList, Version = order.Version + 1 };

        session.Orders.Add(newOrder);
        return session with { Version = session.Version + 1 };
    }

    public async Task<List<PaymentTypeDto>> GetPaymentTypes()
    {
        return _paymentTypeCache.Values.Select(x => PaymentTypeFactory.CreateDto(x)).ToList();
    }
}
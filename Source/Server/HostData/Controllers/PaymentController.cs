using HostData.Cache.Orders;
using HostData.Cache.Payments;
using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal class PaymentController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IPaymentTypeCache _paymentTypeCache;

    public PaymentController(IOrderCache orderCache, IWaiterCache waiterCache, IPaymentTypeCache paymentTypeCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _paymentTypeCache = paymentTypeCache;
    }

    internal async Task<SessionDto> AddPayment(dynamic orderId, dynamic credentialsId, dynamic paymentTypeId, dynamic sum, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var ptId = CheckDynamicGuid(paymentTypeId);

            if (decimal.TryParse(sum.ToString(), out decimal returnSum) is false)
                throw new ArgumentException($"{nameof(sum)} must be type Decimal", nameof(sum));

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            var waiter = CheckCredentials(cId, EmployeePermission.CanAddPaymentOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));

            PaymentTypeDto paymentType = PaymentTypeFactory.CreateDto(_paymentTypeCache.GetPaymentTypeById(ptId));

            var paymentsList = session.Orders.Count <= 0
                ? order.GetPayments()
                : session.Orders.OrderByDescending(x => x.Version).First().GetPayments();

            var paymentDto = new PaymentDto(Guid.NewGuid(), returnSum, paymentType, PaymentStatus.New, false);

            paymentsList.Add(paymentDto);
            var newOrder = order with { Payments = paymentsList, Version = order.Version + 1 };

            session.Orders.Add(newOrder);
            return session with { Version = session.Version + 1 };
        });
    }

    internal async Task<SessionDto> RemovePayment(dynamic orderId, dynamic credentialsId, dynamic paymentId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);
            var cId = CheckDynamicGuid(credentialsId);
            var pId = CheckDynamicGuid(paymentId);

            if (session.OrderId.Equals(oId) is false)
                throw new InvalidSessionException(session.Version, orderId, "Нельзя добавлять в одну сессию разные id");

            var waiter = CheckCredentials(cId, EmployeePermission.CanRemovePaymentOnOrder);

            OrderDto order = OrderFactory.CreateDto(_orderCache.GetOrderById(oId));

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
        });
    }

    internal async Task<List<PaymentTypeDto>> GetPaymentTypes()
    {
        return await Task.Run(() =>
        {
            return _paymentTypeCache.PaymentTypes.Select(x => PaymentTypeFactory.CreateDto(x)).ToList();
        });
    }
}
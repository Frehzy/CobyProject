using HostData.Cache;
using HostData.Cache.Order;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace HostData.Controllers;

internal class OrderController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly IBaseCache<ITable> _tableCache;

    public OrderController(IOrderCache orderCache, IBaseCache<ITable> tableCache, IBaseCache<IWaiter> waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _tableCache = tableCache;
    }

    public Task<OrderDto> GetOrderById(dynamic orderId)
    {
        var oId = CheckDynamicGuid(orderId);

        return Task.FromResult(OrderFactory.CreateDto(_orderCache.GetById(oId)));
    }

    public Task<List<OrderDto>> GetOrders()
    {
        return Task.FromResult(_orderCache.Values.Select(x => OrderFactory.CreateDto(x))
                                                 .ToList());
    }

    public Task<OrderDto> CreateOrder(dynamic credentialsId, dynamic waiterId, IEnumerable<dynamic> tableId)
    {
        var cId = CheckDynamicGuid(credentialsId);
        var wId = CheckDynamicGuid(waiterId);
        var tsId = tableId.Select(x => (Guid)CheckDynamicGuid(x));

        var waiter = CheckCredentials(cId, EmployeePermission.CanCreateOrder);

        var tables = tsId.Select(x => TableFactory.Create(_tableCache.GetById(x)));
        var orderCount = _orderCache.Values.OrderByDescending(x => x.Number).FirstOrDefault()?.Number ?? 0;

        var order = new Order(orderCount + 1, Guid.NewGuid(), tables.ToList(), WaiterFactory.Create(waiter));
        _orderCache.AddOrUpdate(order);
        return Task.FromResult(OrderFactory.CreateDto(order));
    }

    public Task<CredentialsDto> CreateCredentials(dynamic waiterPassword)
    {
        var waiter = WaiterCache.Values.First(x => x.Password.Equals(waiterPassword));
        return Task.FromResult(WaiterFactory.Create(waiter));
    }

    public Task<List<OrderDto>> GetOpenOrders()
    {
        return Task.FromResult(_orderCache.Values.Where(x => x.Status.HasFlag(OrderStatus.Open))
                                                 .Select(x => OrderFactory.CreateDto(x))
                                                 .ToList());
    }

    public Task<OrderDto> SubmitChanges(dynamic credentialsId, SessionDto session)
    {
        var cId = CheckDynamicGuid(credentialsId);

        if (session.Orders.Count <= 0)
            throw new InvalidSessionException(session.Version, session.OrderId);

        CheckCredentials(cId, EmployeePermission.CanUpdateOrder);

        var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();
        PrintAllProduct(lastOrder);

        _orderCache.AddOrUpdate(OrderFactory.Create(lastOrder), session.Orders.Count);
        return Task.FromResult(OrderFactory.CreateDto(_orderCache.GetById(lastOrder.Id)));

        static Task PrintAllProduct(OrderDto order)
        {
            var printAllProduct = order.GetProducts().Where(x => x.Status.HasFlag(ProductStatus.Added));
            printAllProduct = printAllProduct.Select(x => x with
            {
                Status = ProductStatus.Printed,
                PrintTime = DateTime.Now
            });
            return Task.CompletedTask;
        }
    }

    public Task<OrderDto> CloseOrder(dynamic credentialsId, SessionDto session)
    {
        var cId = CheckDynamicGuid(credentialsId);

        if (session.Orders.Count <= 0)
            throw new InvalidSessionException(session.Version, session.OrderId);

        CheckCredentials(cId, EmployeePermission.CanCloseOrder);

        var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();

        if (lastOrder.ResultSum < lastOrder.PaymentsSum)
            throw new CantChangeAndRemoveOrderException(OrderFactory.Create(lastOrder));

        var newOorder = lastOrder with
        {
            CloseTime = DateTime.Now,
            Status = OrderStatus.Closed,
            Payments = lastOrder.GetPayments()
                                .Where(x => x.IsDeleted is false && x.Status.HasFlag(PaymentStatus.New))
                                .Select(x => x with { Status = PaymentStatus.Finished })
                                .ToList(),
            Version = lastOrder.Version + 1,
        };
        session.Orders.Add(newOorder);

        return Task.FromResult(SubmitChanges(credentialsId, session));
    }

    public Task<OrderDto> RemoveOrderById(dynamic credentialsId, dynamic orderId)
    {
        var cId = CheckDynamicGuid(credentialsId);
        var oId = CheckDynamicGuid(orderId);

        CheckCredentials(cId, EmployeePermission.CanRemoveOrder);

        return Task.FromResult(OrderFactory.CreateDto(_orderCache.RemoveById(oId)));
    }
}
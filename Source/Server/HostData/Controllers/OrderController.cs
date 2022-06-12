using HostData.Cache.Orders;
using HostData.Cache.Tables;
using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace HostData.Controllers;

internal class OrderController : BaseController
{
    private readonly IOrderCache _orderCache;
    private readonly ITableCache _tableCache;

    public OrderController(IOrderCache orderCache, ITableCache tableCache, IWaiterCache waiterCache) : base(waiterCache)
    {
        _orderCache = orderCache;
        _tableCache = tableCache;
    }

    public async Task<OrderDto> GetOrderById(dynamic orderId)
    {
        return await Task.Run(() =>
        {
            var oId = CheckDynamicGuid(orderId);

            return OrderFactory.CreateDto(_orderCache.GetOrderById(oId));
        });
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        return await Task.Run(() =>
        {
            return _orderCache.Orders.Select(x => OrderFactory.CreateDto(x)).ToList();
        });
    }

    public async Task<OrderDto> CreateOrder(dynamic credentialsId, dynamic waiterId, IEnumerable<dynamic> tableId)
    {
        return await Task.Run(() =>
        {
            var cId = CheckDynamicGuid(credentialsId);
            var wId = CheckDynamicGuid(waiterId);
            var tsId = tableId.Select(x => (Guid)CheckDynamicGuid(x));

            var waiter = CheckCredentials(cId, EmployeePermission.CanCreateOrder);

            var tables = tsId.Select(x => TableFactory.Create(_tableCache.GetTableById(x)));
            var orderCount = _orderCache.Orders.OrderByDescending(x => x.Number).FirstOrDefault()?.Number ?? 0;

            var order = new Order(orderCount + 1, Guid.NewGuid(), tables.ToList(), WaiterFactory.Create(waiter));
            _orderCache.AddOrUpdate(order);
            return OrderFactory.CreateDto(order);
        });
    }

    public async Task<OrderDto> SubmitChanges(dynamic credentialsId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var cId = CheckDynamicGuid(credentialsId);

            if (session.Orders.Count <= 0)
                throw new InvalidSessionException(session.Version, session.OrderId);

            CheckCredentials(cId, EmployeePermission.CanUpdateOrder);

            var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();
            PrintAllProduct(lastOrder);

            _orderCache.AddOrUpdate(OrderFactory.Create(lastOrder), session.Orders.Count);
            return OrderFactory.CreateDto(_orderCache.GetOrderById(lastOrder.Id));
        });

        static void PrintAllProduct(OrderDto order)
        {
            var printAllProduct = order.GetProducts().Where(x => x.Status.HasFlag(ProductStatus.Added));
            printAllProduct = printAllProduct.Select(x => x with
            {
                Status = ProductStatus.Printed,
                PrintTime = DateTime.Now
            });
        }
    }

    internal async Task<OrderDto> CloseOrder(dynamic credentialsId, SessionDto session)
    {
        return await Task.Run(() =>
        {
            var cId = CheckDynamicGuid(credentialsId);

            if (session.Orders.Count <= 0)
                throw new InvalidSessionException(session.Version, session.OrderId);

            CheckCredentials(cId, EmployeePermission.CanCloseOrder);

            var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();
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

            return SubmitChanges(credentialsId, session);
        });
    }

    public async Task<OrderDto> RemoveOrderById(dynamic credentialsId, dynamic orderId)
    {
        return await Task.Run(() =>
        {
            var cId = CheckDynamicGuid(credentialsId);
            var oId = CheckDynamicGuid(orderId);

            CheckCredentials(cId, EmployeePermission.CanRemoveOrder);

            return OrderFactory.CreateDto(_orderCache.RemoveOrder(oId));
        });
    }
}
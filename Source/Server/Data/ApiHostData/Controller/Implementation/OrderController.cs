using ApiHostData.Cache.Credentials;
using ApiHostData.Controller.Contract;
using ApiHostData.Domain.Models;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Shared.Data.Enum;
using Shared.Factory.Dto;
using SharedData.Mapper;

namespace ApiHostData.Controller.Implementation;

public class OrderController : BaseController, IOrderController
{
    private readonly IOrderService _orderService;
    private readonly ITableService _tableService;

    public OrderController(IOrderService orderService, ITableService tableService, IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
        _orderService = orderService;
        _tableService = tableService;
    }

    public async Task<OrderDto> CreateOrder(dynamic credentials, dynamic waiterId, dynamic tableId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);
        Guid tId = CheckDynamicGuid(tableId);
        var entityThatChanges = await CheckCredentials(cId);

        var table = await _tableService.GetById(tId);
        var waiter = await WaiterService.GetById(wId);
        var lastOrder = await _orderService.GetLastOrder();

        var orderModel = new OrderModel()
        {
            Number = lastOrder?.Number + 1 ?? 1,
            Waiter = waiter,
            Tables = new List<TableModel> { table },
            Guests = new List<GuestModel>(),
            Products = new List<ProductModel>(),
            Discounts = new List<DiscountModel>(),
            Payments = new List<PaymentModel>(),
            Version = 1,
            Status = OrderStatus.Open
        };
        await _orderService.Create(entityThatChanges.Id, orderModel);
        return OrderFactory.CreateDto(orderModel);
    }

    public async Task<List<OrderDto>> GetOpenOrders()
    {
        var ordersModel = await _orderService.GetOpenOrders();
        return ordersModel.Select(x => OrderFactory.CreateDto(x)).ToList();
    }

    public async Task<OrderDto> GetOrderById(dynamic orderId)
    {
        Guid oId = CheckDynamicGuid(orderId);
        var orderModel = await _orderService.GetById(oId);
        return OrderFactory.CreateDto(orderModel);
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        var ordersModel = await _orderService.Get();
        return ordersModel.Select(x => OrderFactory.CreateDto(x)).ToList();
    }

    public async Task<OrderDto> RemoveOrderById(dynamic credentials, dynamic orderId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid oId = CheckDynamicGuid(orderId);

        var entityThatChanges = await CheckCredentials(cId);

        var orderModel = await _orderService.GetById(oId);

        await _orderService.Remove(entityThatChanges.Id, oId);
        return OrderFactory.CreateDto(orderModel);
    }
}
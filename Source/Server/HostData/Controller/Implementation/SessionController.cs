using HostData.Cache.Credentials;
using HostData.Cache.Orders;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
using HostData.Mapper;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class SessionController : BaseController, ISessionController
{
    private readonly IOrderService _orderService;
    private readonly IProductItemService _productItemService;
    private readonly IPaymentTypeService _paymentTypeService;
    private readonly ITableService _tableService;
    private readonly IWaiterService _waiterService;
    private readonly IDiscountTypeService _discountTypeService;

    private readonly ISessionCache _sessionCache;

    public SessionController(IOrderService orderService,
                             IProductItemService productItemService,
                             IPaymentTypeService paymentTypeService,
                             ITableService tableService,
                             IWaiterService waiterService,
                             IDiscountTypeService discountTypeService,
                             IMapper mapper,
                             ICredentialsCache credentialsCache,
                             ISessionCache sessionCache) : base(waiterService, mapper, credentialsCache)
    {
        _orderService = orderService;
        _productItemService = productItemService;
        _paymentTypeService = paymentTypeService;
        _tableService = tableService;
        _waiterService = waiterService;
        _discountTypeService = discountTypeService;
        _sessionCache = sessionCache;
    }

    public async Task<ProductDto> AddCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId, dynamic comment)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid pId = CheckDynamicGuid(productId);
        string comm = Convert.ToString(comment.ToString());
        var entityThatChanges = await CheckCredentials(cId);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var product = order.Products.First(x => x.Id.Equals(pId));
        product.Comment = comm;

        await _sessionCache.Update(order);

        return ProductFactory.CreateDto(product);
    }

    public async Task<DiscountDto> AddDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountTypeId, dynamic sum)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid dtId = CheckDynamicGuid(discountTypeId);
        decimal s = decimal.Parse(sum);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddDiscountOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var discountType = await _discountTypeService.GetById(dtId);
        var discountModel = new DiscountModel()
        {
            Discount = discountType,
            DiscountSum = s
        };
        order.Discounts.Add(discountModel);

        await _sessionCache.Update(order);
        return DiscountFactory.CreateDto(discountModel);
    }

    public async Task<PaymentDto> AddPayment(dynamic credentialsId, dynamic sessionId, dynamic paymentTypeId, dynamic sum)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid ptId = CheckDynamicGuid(paymentTypeId);
        decimal s = decimal.Parse(sum);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddPaymentOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var paymentType = await _paymentTypeService.GetById(ptId);
        var payment = new PaymentModel()
        {
            Type = paymentType,
            Status = PaymentStatus.New,
            Sum = s
        };
        order.Payments.Add(payment);

        await _sessionCache.Update(order);
        return PaymentFactory.CreateDto(payment);
    }

    public async Task<ProductDto> AddProduct(dynamic credentialsId, dynamic sessionId, dynamic guestId, dynamic productItemId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid gId = CheckDynamicGuid(guestId);
        Guid piId = CheckDynamicGuid(productItemId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddDishesOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var guest = order.Guests.First(x => x.Id.Equals(gId));
        var productItem = await _productItemService.GetById(piId);
        var product = new ProductModel()
        {
            WaiterId = entityThatChanges.Id,
            ProductItem = productItem,
            GuestId = guest.Id,
            Status = ProductStatus.Added
        };
        order.Products.Add(product);

        await _sessionCache.Update(order);
        return ProductFactory.CreateDto(product);
    }

    public async Task<TableDto> ChangeTable(dynamic credentialsId, dynamic sessionId, dynamic tableId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid tId = CheckDynamicGuid(tableId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanChangeTableOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var table = await _tableService.GetById(tId);
        order.Tables.Clear();
        order.Tables.Add(table);

        await _sessionCache.Update(order);
        return TableFactory.CreateDto(table);
    }

    public async Task<WaiterDto> ChangeWaiter(dynamic credentialsId, dynamic sessionId, dynamic waiterId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid wId = CheckDynamicGuid(waiterId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanChangeWaiterOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var waiter = await _waiterService.GetById(wId);
        order.Waiter = waiter;

        await _sessionCache.Update(order);
        return WaiterFactory.CreateDto(waiter);
    }

    public async Task<OrderDto> CloseOrder(dynamic credentialsId, dynamic sessionId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanCloseOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        order.CloseTime = DateTime.Now;
        order.Payments.Where(x => x.IsDeleted is false).ToList().ForEach(x => x.Status = PaymentStatus.Finished);
        order.Products.Where(x => x.IsDeleted is false).ToList().ForEach(x =>
        {
            x.Status = ProductStatus.Served;
            x.PrintTime = DateTime.Now;
        });
        order.Status = OrderStatus.Closed;

        await _sessionCache.Update(order);
        return OrderFactory.CreateDto(order);
    }

    public async Task<OrderDto> DeleteOrder(dynamic credentialsId, dynamic sessionId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemoveOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        order.Status = OrderStatus.Deleted;

        await _sessionCache.Update(order);
        return OrderFactory.CreateDto(order);
    }

    public async Task<ProductDto> RemoveCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid pId = CheckDynamicGuid(productId);
        var entityThatChanges = await CheckCredentials(cId);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var product = order.Products.First(x => x.Id.Equals(pId));
        product.Comment = string.Empty;

        await _sessionCache.Update(order);
        return ProductFactory.CreateDto(product);
    }

    public async Task<DiscountDto> RemoveDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid dId = CheckDynamicGuid(discountId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemoveDiscountOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var discount = order.Discounts.Where(x => x.IsDeleted is false).First(x => x.Id.Equals(dId));
        discount.IsActive = false;

        await _sessionCache.Update(order);
        return DiscountFactory.CreateDto(discount);
    }

    public async Task<PaymentDto> RemovePayment(dynamic credentialsId, dynamic sessionId, dynamic paymentId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid pId = CheckDynamicGuid(paymentId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePaymentOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var payment = order.Payments.Where(x => x.IsDeleted == false && x.Status.HasFlag(PaymentStatus.New))
                                    .First(x => x.Id.Equals(pId));
        payment.Status = PaymentStatus.Removed;

        await _sessionCache.Update(order);
        return PaymentFactory.CreateDto(payment);
    }

    public async Task<ProductDto> RemoveProduct(dynamic credentialsId, dynamic sessionId, dynamic productId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        Guid pId = CheckDynamicGuid(productId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePrintedDishesOnOrder);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var order = orderVersion.Order;
        await CheckIfOrderIsClosed(order);
        var product = order.Products.Where(x => x.IsDeleted == false && x.Status.HasFlag(ProductStatus.Deleted) is false)
                                    .First(x => x.Id.Equals(pId));

        if (product.Status.HasFlag(ProductStatus.Added))
            await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemoveDishesOnOrder);
        else
            await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePrintedDishesOnOrder);

        product.Status = ProductStatus.Deleted;

        await _sessionCache.Update(order);
        return ProductFactory.CreateDto(product);
    }

    public async Task<OrderDto> SubmitChanges(dynamic credentialsId, dynamic sessionId)
    {
        Guid cId = CheckDynamicGuid(credentialsId);
        Guid sId = CheckDynamicGuid(sessionId);
        var entityThatChanges = await CheckCredentials(cId);

        var orderVersion = await _sessionCache.GetBySessionId(sId);
        var orderOnCache = orderVersion.Order;
        var order = await _orderService.GetById(orderOnCache.Id);

        if (order.Version != orderOnCache.Version - orderVersion.SessionVersion)
            throw new InvalidSessionException(orderVersion.SessionVersion, orderOnCache.Id);

        await _orderService.Update(entityThatChanges.Id, orderOnCache);

        await _sessionCache.RemoveBySessionId(sId);
        return OrderFactory.CreateDto(orderOnCache);
    }
}
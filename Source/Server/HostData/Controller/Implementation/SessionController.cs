using HostData.Cache.Credentials;
using HostData.Cache.Orders;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
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
    private readonly IMapper _mapper;

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
        _mapper = mapper;
        _sessionCache = sessionCache;
    }

    public async Task<ProductDto> AddCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId, dynamic comment)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var pId = (Guid)CheckDynamicGuid(productId);
        var comm = (string)Convert.ToString(comment);
        var entityThatChanges = await CheckCredentials(cId);

        var order = await _sessionCache.GetBySessionId(sId);
        var product = order.Products.First(x => x.Id.Equals(pId));
        product.Comment = comm;

        await _sessionCache.Update(order);

        return ProductMapper.CreateDto(product);
    }

    public async Task<DiscountDto> AddDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountTypeId, dynamic sum)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var dtId = (Guid)CheckDynamicGuid(discountTypeId);
        var s = (decimal)decimal.Parse(sum);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddDiscountOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var discountType = await _discountTypeService.GetById(dtId);
        var discountModel = new DiscountModel()
        {
            Discount = discountType,
            DiscountSum = s
        };
        order.Discounts.Add(discountModel);

        await _sessionCache.Update(order);
        return DiscountMapper.CreateDto(discountModel);
    }

    public async Task<PaymentDto> AddPayment(dynamic credentialsId, dynamic sessionId, dynamic paymentTypeId, dynamic sum)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var ptId = (Guid)CheckDynamicGuid(paymentTypeId);
        var s = (decimal)decimal.Parse(sum);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddPaymentOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var paymentType = await _paymentTypeService.GetById(ptId);
        var payment = new PaymentModel()
        {
            Type = paymentType,
            Status = PaymentStatus.New,
            Sum = s
        };
        order.Payments.Add(payment);

        await _sessionCache.Update(order);
        return PaymentMapper.CreateDto(payment);
    }

    public async Task<ProductDto> AddProduct(dynamic credentialsId, dynamic sessionId, dynamic guestId, dynamic productItemId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var gId = (Guid)CheckDynamicGuid(guestId);
        var piId = (Guid)CheckDynamicGuid(productItemId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanAddDishesOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
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
        return ProductMapper.CreateDto(product);
    }

    public async Task<TableDto> ChangeTable(dynamic credentialsId, dynamic sessionId, dynamic tableId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var tId = (Guid)CheckDynamicGuid(tableId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanChangeTableOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var table = await _tableService.GetById(tId);
        order.Tables.Clear();
        order.Tables.Add(table);

        await _sessionCache.Update(order);
        return Mapper.Map<TableModel, TableDto>(table);
    }

    public async Task<WaiterDto> ChangeWaiter(dynamic credentialsId, dynamic sessionId, dynamic waiterId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanChangeWaiterOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var waiter = await _waiterService.GetById(wId);
        order.Waiter = waiter;

        await _sessionCache.Update(order);
        return Mapper.Map<WaiterModel, WaiterDto>(waiter);
    }

    public async Task<OrderDto> CloseOrder(dynamic credentialsId, dynamic sessionId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanCloseOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        order.CloseTime = DateTime.Now;
        order.Payments.Where(x => x.IsDeleted is false).ToList().ForEach(x => x.Status = PaymentStatus.Finished);
        order.Products.Where(x => x.IsDeleted is false).ToList().ForEach(x =>
        {
            x.Status = ProductStatus.Served;
            x.PrintTime = DateTime.Now;
        });
        order.Status = OrderStatus.Closed;

        await _sessionCache.Update(order);
        return OrderMapper.CreateDto(order);
    }

    public async Task<ProductDto> RemoveCommentOnProduct(dynamic credentialsId, dynamic sessionId, dynamic productId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var pId = (Guid)CheckDynamicGuid(productId);
        var entityThatChanges = await CheckCredentials(cId);

        var order = await _sessionCache.GetBySessionId(sId);
        var product = order.Products.First(x => x.Id.Equals(pId));
        product.Comment = string.Empty;

        await _sessionCache.Update(order);
        return ProductMapper.CreateDto(product);
    }

    public async Task<DiscountDto> RemoveDiscount(dynamic credentialsId, dynamic sessionId, dynamic discountId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var dId = (Guid)CheckDynamicGuid(discountId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemoveDiscountOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var discount = order.Discounts.Where(x => x.IsDeleted is false).First(x => x.Id.Equals(dId));
        discount.IsActive = false;

        await _sessionCache.Update(order);
        return DiscountMapper.CreateDto(discount);
    }

    public async Task<PaymentDto> RemovePayment(dynamic credentialsId, dynamic sessionId, dynamic paymentId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var pId = (Guid)CheckDynamicGuid(paymentId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePaymentOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var payment = order.Payments.Where(x => x.IsDeleted == false && x.Status.HasFlag(PaymentStatus.New))
                                    .First(x => x.Id.Equals(pId));
        payment.Status = PaymentStatus.Removed;

        await _sessionCache.Update(order);
        return PaymentMapper.CreateDto(payment);
    }

    public async Task<ProductDto> RemoveProduct(dynamic credentialsId, dynamic sessionId, dynamic productId)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var pId = (Guid)CheckDynamicGuid(productId);
        var entityThatChanges = await CheckCredentials(cId);
        await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePrintedDishesOnOrder);

        var order = await _sessionCache.GetBySessionId(sId);
        var product = order.Products.Where(x => x.IsDeleted == false && x.Status.HasFlag(ProductStatus.Deleted) is false)
                                    .First(x => x.Id.Equals(pId));

        if (product.Status.HasFlag(ProductStatus.Added))
            await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemoveDishesOnOrder);
        else
            await CheckPermission(entityThatChanges.Id, EmployeePermission.CanRemovePrintedDishesOnOrder);

        product.Status = ProductStatus.Deleted;

        await _sessionCache.Update(order);
        return ProductMapper.CreateDto(product);
    }

    public async Task<OrderDto> SubmitChanges(dynamic credentialsId, dynamic sessionId, dynamic version)
    {
        var cId = (Guid)CheckDynamicGuid(credentialsId);
        var sId = (Guid)CheckDynamicGuid(sessionId);
        var v = (int)int.Parse(version);
        var entityThatChanges = await CheckCredentials(cId);

        var orderOnCache = await _sessionCache.GetBySessionId(sId);
        var order = await _orderService.GetById(orderOnCache.Id);

        if (order.Version != orderOnCache.Version - v)
            throw new InvalidSessionException(v, orderOnCache.Id);

        await _orderService.Update(entityThatChanges.Id, orderOnCache);

        await _sessionCache.RemoveBySessionId(sId);
        return OrderMapper.CreateDto(orderOnCache);
    }
}
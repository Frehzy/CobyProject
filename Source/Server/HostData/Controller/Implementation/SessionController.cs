using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
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

    public SessionController(IOrderService orderService,
                             IProductItemService productItemService,
                             IPaymentTypeService paymentTypeService,
                             ITableService tableService,
                             IWaiterService waiterService,
                             IDiscountTypeService discountTypeService,
                             IMapper mapper,
                             ICredentialsCache credentialsCache) : base(waiterService, mapper, credentialsCache)
    {
        _orderService = orderService;
        _productItemService = productItemService;
        _paymentTypeService = paymentTypeService;
        _tableService = tableService;
        _waiterService = waiterService;
        _discountTypeService = discountTypeService;
        _mapper = mapper;
    }

    public async Task<SessionDto> AddCommentOnProduct(object session, dynamic credentialsId, dynamic productId, string comment)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> AddDiscount(object session, dynamic credentialsId, dynamic discountTypeId, dynamic sum)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> AddPayment(object session, dynamic credentialsId, dynamic paymentTypeId, decimal sum)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> AddProduct(object session, dynamic credentialsId, dynamic guestId, dynamic productItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> ChangeTable(object session, dynamic credentialsId, dynamic tableId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> ChangeWaiter(object session, dynamic credentialsId, dynamic waiterId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> CloseOrder(object session, dynamic credentialsId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> RemoveCommentOnProduct(object session, dynamic credentialsId, dynamic productId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> RemoveDiscount(object session, dynamic credentialsId, dynamic discountId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> RemovePayment(object session, dynamic credentialsId, dynamic paymentId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> RemoveProduct(object session, dynamic credentialsId, dynamic productId)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> SubmitChanges(object session, dynamic credentialsId)
    {
        throw new NotImplementedException();
    }
}
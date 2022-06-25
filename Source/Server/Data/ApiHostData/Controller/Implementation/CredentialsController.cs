using ApiHostData.Cache.Credentials;
using ApiHostData.Cache.Session;
using ApiHostData.Controller.Contract;
using ApiHostData.Services.Contract;
using Shared.Factory.Dto;
using SharedData.Mapper;

namespace ApiHostData.Controller.Implementation;

public class CredentialsController : BaseController, ICredentialsController
{
    private readonly ISessionCache _sessionCache;
    private readonly IOrderService _orderService;

    public CredentialsController(IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache, ISessionCache sessionCache, IOrderService orderService)
        : base(waiterService, mapper, credentialsCache)
    {
        _sessionCache = sessionCache;
        _orderService = orderService;
    }

    public async Task<LicenceDto> CheckLicence(dynamic moduleLicenceId)
    {
        int mLId = int.Parse(moduleLicenceId);
        //тут нужен запрос в базу данных
        return new LicenceDto(default, default, default);
    }

    public async Task<CredentialsDto> CreateCredentials(dynamic password)
    {
        string p = Convert.ToString(password.ToString());

        var waiters = await WaiterService.Get();
        var waiterModule = waiters.First(x => x.Password.Equals(p));
        return await CredentialsCache.TryAdd(waiterModule);
    }

    public async Task<SessionDto> CreateSession(dynamic orderId)
    {
        Guid oId = CheckDynamicGuid(orderId);

        var orderModule = await _orderService.GetById(oId);
        return await _sessionCache.TryAdd(orderModule);
    }
}
using ApiHostData.Cache.Credentials;
using ApiHostData.Cache.Session;
using ApiHostData.Controller.Contract;
using ApiHostData.Services.Contract;
using Shared.Configuration;
using Shared.Exceptions;
using Shared.Factory.Dto;
using SharedData.Mapper;
using System.Net;
using System.Text.Json;

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

    public async Task<List<LicenceDto>> CheckLicence(dynamic organizationId, dynamic moduleLicenceId)
    {
        Guid oId = CheckDynamicGuid(organizationId);
        int mLId = int.Parse(moduleLicenceId);
        return await GetLicences(oId, mLId);

        async Task<List<LicenceDto>> GetLicences(Guid organizationId, int moduleLicenceId)
        {
            var ip = NetOperation.GetLocalIPAddress();
            var uri = new Uri($"http://{ip}:5051/{organizationId}/{moduleLicenceId}");
            using var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.StatusCode is not HttpStatusCode.OK)
                throw new InvalidLicenceModuleException();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<LicenceDto>>(json);
        }
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
using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class CredentialsController : BaseController, ICredentialsController
{
    public CredentialsController(IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
    }

    public async Task<CredentialsDto> CreateCredentials(dynamic password)
    {
        var p = (string)Convert.ToString(password);

        var waiters = await WaiterService.GetAll();
        var waiterModule = waiters.First(x => x.Password.Equals(p));
        return await CredentialsCache.TryAdd(waiterModule);
    }
}
using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;

namespace HostData.Controller.Implementation;

public class SessionController : BaseController, ISessionController
{
    public SessionController(IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
    }
}
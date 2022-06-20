using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class SessionService : BaseService, ISessionService
{
    public SessionService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }
}
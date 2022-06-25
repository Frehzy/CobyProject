using HostData.Domain.Context;
using HostData.Repository.Contracts;

namespace HostData.Repository.Implementation;

public sealed class ApiHostRepository : BaseRepository<ApiHostServicesDataContext>, IApiHostRepository
{
    public ApiHostRepository(ApiHostServicesDataContext context) : base(context)
    {
    }
}
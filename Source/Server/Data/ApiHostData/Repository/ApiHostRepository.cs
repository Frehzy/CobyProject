using SharedData.Repository;

namespace ApiHostData.Repository;

public sealed class ApiHostRepository : BaseRepository<ApiHostServicesDataContext>, IApiHostRepository
{
    public ApiHostRepository(ApiHostServicesDataContext context) : base(context)
    {
    }
}
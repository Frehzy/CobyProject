using HostData.Domain.Context;
using HostData.Repository.Contracts;

namespace HostData.Repository.Implementation;

public sealed class LicenceRepository : BaseRepository<LicenceServicesDataContext>, ILicenceRepository
{
    public LicenceRepository(LicenceServicesDataContext context) : base(context)
    {
    }
}
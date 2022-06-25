using SharedData.Repository;

namespace LicenceData.Repository;

public sealed class LicenceRepository : BaseRepository<LicenceServicesDataContext>, ILicenceRepository
{
    public LicenceRepository(LicenceServicesDataContext context) : base(context)
    {
    }
}
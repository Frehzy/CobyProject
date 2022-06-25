using LicenceData.Domain.Entities;
using LicenceData.Domain.Models;
using LicenceData.Repository;
using LicenceData.Services.Contract;
using SharedData.Mapper;

namespace LicenceData.Services.Implementation;

public class LicenceService : ILicenceService
{
    private readonly ILicenceRepository _dbRepository;
    private readonly IMapper _mapper;

    public LicenceService(ILicenceRepository dbRepository, IMapper mapper)
    {
        _dbRepository = dbRepository;
        _mapper = mapper;
    }

    public async Task<List<LicenceModel>> Get(Guid organizationId, int moduleId)
    {
        var collection = await _dbRepository.Get<LicenceEntity>(x => x.OrganizationId.Equals(organizationId) && x.ModuleLicenceId.Equals(moduleId));
        return _mapper.Map<LicenceEntity, LicenceModel>(collection).ToList();
    }
}
using LicenceData.Controller.Contract;
using LicenceData.Services.Contract;
using Shared.Exceptions;
using Shared.Factory.Dto;

namespace LicenceData.Controller.Implementation;

public class LicenceController : ILicenceController
{
    private readonly ILicenceService _licenceService;

    public LicenceController(ILicenceService licenceService)
    {
        _licenceService = licenceService;
    }

    public async Task<List<LicenceDto>> GetLicence(dynamic organizationId, dynamic moduleLicenceId)
    {
        Guid orgId = Guid.TryParse(organizationId.ToString(), out Guid returnGuid) is true
                    ? returnGuid
                    : throw new ArgumentException($"{nameof(organizationId)} must be type Guid", nameof(organizationId));
        int modLicId = Convert.ToInt32((int)moduleLicenceId);

        if (modLicId == 5050) //тестовая лицензия на 10 слотов
            orgId = Guid.Empty;

        var licenceModels = await _licenceService.Get(orgId, modLicId);
        if (licenceModels.Count <= 0)
            throw new InvalidLicenceModuleException();

        return licenceModels.Select(x => new LicenceDto(returnGuid, x.ModuleLicenceId, x.MaxReservedLicence)).ToList();
    }
}
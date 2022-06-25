using HostData.Controller.Contract;
using HostData.Domain.Contracts.Services;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

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
        int modLicId = Convert.ToInt32(moduleLicenceId);

        var licenceModels = await _licenceService.Get(orgId, modLicId);
        return licenceModels.Select(x => new LicenceDto(x.OrganizationId, x.ModuleLicenceId, x.MaxReservedLicence)).ToList();
    }
}
using HostData.Controller.Contract;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class LicenceModule : BaseModule
{
    private readonly ILicenceController _licenceController;

    public LicenceModule(ILicenceController licenceController) : base()
    {
        _licenceController = licenceController;

        Get("/{organizationId}/{moduleLicenceId}", async parameters =>
        {
            var organizationId = parameters.organizationId;
            var moduleLicenceId = parameters.moduleLicenceId;
            return await Execute<LicenceDto>(Context, () => _licenceController.GetLicence(organizationId, moduleLicenceId));
        });
    }
}
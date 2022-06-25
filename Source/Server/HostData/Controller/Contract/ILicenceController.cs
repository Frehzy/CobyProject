using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface ILicenceController
{
    Task<List<LicenceDto>> GetLicence(dynamic organizationId, dynamic moduleLicenceId);
}
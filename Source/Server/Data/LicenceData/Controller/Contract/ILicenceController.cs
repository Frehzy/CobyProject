using Shared.Factory.Dto;

namespace LicenceData.Controller.Contract;

public interface ILicenceController
{
    Task<List<LicenceDto>> GetLicence(dynamic organizationId, dynamic moduleLicenceId);
}
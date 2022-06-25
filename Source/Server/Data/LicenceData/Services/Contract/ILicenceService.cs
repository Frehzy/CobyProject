using LicenceData.Domain.Models;

namespace LicenceData.Services.Contract;

public interface ILicenceService
{
    Task<List<LicenceModel>> Get(Guid organizationId, int moduleId);
}
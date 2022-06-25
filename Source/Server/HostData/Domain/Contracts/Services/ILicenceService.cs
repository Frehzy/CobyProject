using HostData.Domain.Contracts.Models;

namespace HostData.Domain.Contracts.Services;

public interface ILicenceService
{
    Task<List<LicenceModel>> Get(Guid organizationId, int moduleId);
}
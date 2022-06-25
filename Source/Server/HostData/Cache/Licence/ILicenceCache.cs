using HostData.Cache.Entities;

namespace HostData.Cache.Licence;

public interface ILicenceCache
{
    public IReadOnlyList<LicenceAction> Licences { get; }

    public bool AddLicence(int moduleLicenceId, string terminalId, string organizationId);

    public bool RemoveLicence(string terminalId);
}
using HostData.Cache.Entities;

namespace HostData.Cache.Licence;

public interface ILicenceCache
{
    public IReadOnlyList<LicenceEntity> Licences { get; }

    public bool AddLicence(int moduleLicenceId, string terminalId);

    public bool RemoveLicence(string terminalId);
}
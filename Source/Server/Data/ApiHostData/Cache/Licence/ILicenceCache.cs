using ApiHostData.Cache.Entities;

namespace ApiHostData.Cache.Licence;

public interface ILicenceCache
{
    public IReadOnlyList<LicenceAction> Licences { get; }

    public bool AddLicence(int moduleLicenceId, string terminalId, string organizationId);

    public bool RemoveLicence(string terminalId);
}
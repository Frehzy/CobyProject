using ApiHostData.Cache.Entities;
using Shared.Factory.Dto;

namespace ApiHostData.Cache.Licence;

public interface ILicenceCache
{
    public IReadOnlyList<LicenceAction> Licences { get; }

    public bool AddLicence(string terminalId, LicenceDto licence);

    public bool RemoveLicence(string terminalId);
}
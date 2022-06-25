namespace HostData.Domain.Contracts.Models;

public class LicenceModel : BaseModel
{
    public Guid OrganizationId { get; set; }

    public int ModuleLicenceId { get; set; }

    public int MaxReservedLicence { get; set; }

    public LicenceModel() { }
}
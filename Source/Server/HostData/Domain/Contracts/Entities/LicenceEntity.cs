namespace HostData.Domain.Contracts.Entities;

public class LicenceEntity : BaseEntity
{
    public Guid OrganizationId { get; set; }

    public int ModuleLicenceId { get; set; }

    public int MaxReservedLicence { get; set; }

    public LicenceEntity() { }
}
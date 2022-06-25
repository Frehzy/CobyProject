namespace Shared.Data;

public interface IConfigSettings
{
    public Guid OrganizationId { get; }

    public Guid TerminalId { get; }
}
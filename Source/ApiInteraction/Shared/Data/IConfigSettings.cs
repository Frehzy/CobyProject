namespace Shared.Data;

public interface IConfigSettings
{
    public Guid OrganizationId { get; }

    public void Update();
}
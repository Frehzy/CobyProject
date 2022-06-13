using Shared.Data.Enum;

namespace Shared.Data;

public interface IWaiter
{
    public Guid Id { get; }

    public string Name { get; }

    public bool IsSessionOpen { get; }

    public IReadOnlyList<EmployeePermission> Permissions { get; }

    public bool IsDeleted { get; }

    public IReadOnlyList<EmployeePermission> GetPermissions();
}
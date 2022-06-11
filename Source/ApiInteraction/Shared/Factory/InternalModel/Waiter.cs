using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Waiter : IWaiter
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsSessionOpen { get; set; }

    public IReadOnlyList<EmployeePermission> Permissions { get; set; }

    public Waiter() { }

    public Waiter(Guid id, string name, bool isSessionOpen, List<EmployeePermission> permissions)
    {
        Id = id;
        Name = name;
        IsSessionOpen = isSessionOpen;
        Permissions = permissions.ToList();
    }

    public IReadOnlyList<EmployeePermission> GetPermissions() =>
        (Permissions ?? Enumerable.Empty<EmployeePermission>()).ToList();
}